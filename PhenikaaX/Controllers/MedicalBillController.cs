using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhenikaaX.Entities;
using PhenikaaX.IService;
using PhenikaaX.IServices;
using PhenikaaX.Models;

namespace PhenikaaX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalBillController : ControllerBase
    {
        private readonly PhenikaaXContext _phenikaaXContext;
        private readonly IMedicalBillService _medicalBillService;
        private readonly IUnitOfWork _unitOfWork;

        public MedicalBillController(PhenikaaXContext phenikaaXContext, IMedicalBillService medicalBillService, IUnitOfWork unitOfWork)
        {
            _phenikaaXContext = phenikaaXContext;
            _medicalBillService = medicalBillService;
            _unitOfWork = unitOfWork;
        }

        #region patient
        [HttpGet("get-patient-by-id")]
        public async Task<IActionResult> GetPatientById([FromQuery] Guid patientId)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(patientId);
            if (patient == null) throw new Exception($"Patient had id is {patientId} not found");
            return Ok(patient);
        }

        [HttpPost("insert-patient")]
        public async Task<ActionResult> InsertPatient([FromBody] Patient patient)
        {
            patient.PatientId = Guid.NewGuid();
            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();
            return Ok($"You added patient with id {patient.PatientId}");
        }

        [HttpDelete("delete-patient-by-id")]
        public async Task<ActionResult> DeletePatientById([FromQuery] Guid patientId)
        {
            Patient? patient = await _unitOfWork.Patients.GetByIdAsync(patientId);
            if (patient == null) throw new Exception($"Patient had id is {patientId} not found");
            await _unitOfWork.Patients.DeleteAsync(patientId);
            await _unitOfWork.SaveChangesAsync();
            return Ok($"You removed patient with id {patientId}");
        }

        [HttpGet("get-all-patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            return Ok(patients);
        }
        #endregion

        #region diagnose
        [HttpGet("get-diagnose-by-id")]
        public async Task<IActionResult> GetDiagnoseById([FromQuery] Guid diagnoseId)
        {
            var diagnose = await _unitOfWork.Diagnoses.GetByIdAsync(diagnoseId);
            if (diagnose == null) throw new Exception($"Patient had id is {diagnoseId} not found");
            return Ok(diagnose);
        }

        [HttpPost("insert-diagnose")]
        public async Task<ActionResult> InsertDiagnose([FromBody] Diagnose diagnose)
        {
            diagnose.DiagnoseId = Guid.NewGuid();
            await _unitOfWork.Diagnoses.AddAsync(diagnose);
            return Ok($"You added Diagnose with id {diagnose.DiagnoseId}");
        }

        [HttpDelete("delete-diagnose-by-id")]
        public async Task<ActionResult<string>> DeleteDiagnoseById([FromQuery] Guid diagnoseId)
        {
            Diagnose? diagnose = await _unitOfWork.Diagnoses.GetByIdAsync(diagnoseId);
            if (diagnose == null) throw new Exception($"Diagnose had id is {diagnoseId} not found");
            await _unitOfWork.Diagnoses.DeleteAsync(diagnoseId);
            return Ok($"You removed Diagnose with id {diagnoseId}");
        }

        [HttpGet("get-all-diagnoses")]
        public async Task<IEnumerable<Diagnose>> GetAllDiagnoses()
        {
            return await _unitOfWork.Diagnoses.GetAllAsync();
        }
        #endregion

        #region print pdf
        [HttpPost("print-medical-bill-to-pdf")]
        public async Task<ActionResult> PrintMedicalBillToPdf(List<MedicalInforDataPost>? dataPost)
        {
            if (dataPost == null || dataPost.Count == 0 || string.IsNullOrEmpty(dataPost[0].ReportParamData))
            {
                return BadRequest("Invalid data");
            }

            ReportParamData? reportParamData = JsonConvert.DeserializeObject<ReportParamData>(dataPost[0].ReportParamData);
            if (reportParamData == null)
                return BadRequest("Invalid ReportParamData JSON");

            MedicalBillInfor medicalBillDataPost = new MedicalBillInfor
            {
                ReportTypeCode = dataPost[0].ReportTypeCode,
                ReportParamData = reportParamData,
                PatientReceptionId = dataPost[0].PatientReceptionId,
                PatientDesignateServiceId = dataPost[0].PatientDesignateServiceId,
                BasicInformationPatientId = dataPost[0].BasicInformationPatientId,
                PatientId = dataPost[0].PatientId
            };
            return Ok(await _medicalBillService.CreateMedicalBillToPdf(medicalBillDataPost));
        }
        #endregion
    }
}
