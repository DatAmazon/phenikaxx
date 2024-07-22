

using PhenikaaX.DTOs;
using PhenikaaX.Models;
using Spire.Doc;

namespace PhenikaaX.Intefaces
{
    public interface IMedicalBilllWordDocumentBuilder
    {
        IMedicalBilllWordDocumentBuilder Title(PatientDto patientDto, MedicalBillInfor medicalBillInfor);
        IMedicalBilllWordDocumentBuilder Administrative(PatientDto patientDto);
        IMedicalBilllWordDocumentBuilder MedicalExaminationInformation(PatientDto patientDto, MedicalBillInfor medicalBillInfor, List<DiagnoseDto> lstDiagnosesDto);
        IMedicalBilllWordDocumentBuilder Solve(PatientDto patientDto, MedicalBillInfor medicalBillInfor);
        void SavePdf();
    }
}
