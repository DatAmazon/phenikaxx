using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using PhenikaaX.DTOs;
using PhenikaaX.Entities;
using PhenikaaX.Intefaces;
using PhenikaaX.Interfaces;
using PhenikaaX.Models;
using PhenikaaX.Utils;
using Spire.Barcode;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;

namespace PhenikaaX.Services
{
    public class MedicalBillWordDocumentBuilder : IMedicalBilllWordDocumentBuilder
    {
        private readonly IMedicalBillWordDocumentService _medicalBillWordDocumentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MedicalBillWordDocumentBuilder(IMedicalBillWordDocumentService medicalBillWordDocumentService, IWebHostEnvironment webHostEnvironment)
        {
            _medicalBillWordDocumentService = medicalBillWordDocumentService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IMedicalBilllWordDocumentBuilder Title(PatientDto patientDto, MedicalBillInfor medicalBillInfor)
        {
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 0, cellIndex = 0, leftIndent = 0, text = ConstantManager.HEALTH_DEPARTMENT_HANOI, fontSize = 9.5f, alignment = HorizontalAlignment.Center });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 0, cellIndex = 1, leftIndent = 0, text = ConstantManager.MEDICAL_EXAMINATION_FORM, fontSize = 12.5f, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 0, cellIndex = 2, leftIndent = 0, text = ConstantManager.TICKET_NUMBER, fontSize = 9.5f, alignment = HorizontalAlignment.Center, binding = patientDto?.OrderNumber?.ToString() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 1, cellIndex = 0, leftIndent = 0, text = ConstantManager.HOSPITAL_NAME, fontSize = 9f, bold = true, alignment = HorizontalAlignment.Center, binding = "\r\nPHENIKAA HOÀNG NGÂN", underlineStyle = UnderlineStyle.Single, boldBinding = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 1, cellIndex = 2, leftIndent = 0, text = ConstantManager.PATIENT_CODE, fontSize = 9.5f, alignment = HorizontalAlignment.Center, binding = medicalBillInfor.PatientId.ToString().Substring(0, 8) });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 2, cellIndex = 0, leftIndent = 0, text = ConstantManager.HOTLINE, fontSize = 9.5f, textColor = Color.Red, alignment = HorizontalAlignment.Center });
            _medicalBillWordDocumentService.InsertTextAndCheckbox(new TextAndCheckboxOptions
            {
                RowIndex = 2,
                ColIndex = 1,
                Elements = new List<Element>
                {
                    new Element { Text = ConstantManager.TYPE_NORMAL, FontSize = 9.5f, ImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", patientDto?.Type == 1 ? ConstantManager.CHECKED : ConstantManager.UNCHECK), IsItalic = false },
                    new Element { Text = ConstantManager.EMERGENCY_TYPE, FontSize = 9.5f, ImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", patientDto?.Type == 2 ? ConstantManager.CHECKED : ConstantManager.UNCHECK), IsItalic = false }
                }
            });
            return this;
        }
        public IMedicalBilllWordDocumentBuilder Administrative(PatientDto patientDto)
        {
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 3, cellIndex = 0, leftIndent = 0, text = ConstantManager.MEDICAL_INFO_TITLE, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 4, cellIndex = 0, leftIndent = 10, text = ConstantManager.NAME_IN_UPPER_CASE, fontSize = 11, binding = patientDto?.Username?.ToUpper() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 4, cellIndex = 1, leftIndent = 20, text = $"{ConstantManager.BIRTHDAY + patientDto?.Birthday?.Day.ToString()}/{patientDto?.Birthday?.Month.ToString()}{patientDto?.Birthday?.Year.ToString()}", fontSize = 11 });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 4, cellIndex = 2, leftIndent = 10, text = ConstantManager.AGE, fontSize = 11, binding = patientDto?.Age?.ToString() });
            _medicalBillWordDocumentService.GenderAndJob(patientDto?.Gender, patientDto?.Job);
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 5, cellIndex = 1, leftIndent = 20, text = ConstantManager.ETHNIC, fontSize = 11, binding = patientDto?.Ethnic?.ToString() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 5, cellIndex = 2, leftIndent = 10, text = ConstantManager.NATIONALITY, fontSize = 11, binding = patientDto?.Nationality?.ToString() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 6, cellIndex = 0, leftIndent = 10, text = ConstantManager.ADDRESS, fontSize = 11, binding = patientDto?.Address?.ToString() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 7, cellIndex = 0, leftIndent = 10, text = ConstantManager.WORKPLACE, fontSize = 11, binding = patientDto?.Workplace?.ToString() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 7, cellIndex = 1, leftIndent = 20, text = ConstantManager.PHONE, fontSize = 11, binding = patientDto?.Phone?.ToString() });

            _medicalBillWordDocumentService.MergeCellHorizontal(8, (0, CellMerge.Start), (1, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertTextAndCheckbox(new TextAndCheckboxOptions
            {
                RowIndex = 8,
                ColIndex = 0,
                Title = ConstantManager.SUBJECT_TITLE,
                Elements = new List<Element>    {
               new Element { Text = ConstantManager.HEALTH_INSURANCE, FontSize = 11f, ImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", patientDto?.Subject == 1 ? ConstantManager.CHECKED : ConstantManager.UNCHECK), IsItalic = true },
               new Element { Text = ConstantManager.FEE, FontSize = 11f, ImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", patientDto?.Subject == 2 ? ConstantManager.CHECKED : ConstantManager.UNCHECK), IsItalic = true },
               new Element { Text = ConstantManager.FREE, FontSize = 11f, ImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", patientDto?.Subject == 1 ? ConstantManager.CHECKED : ConstantManager.UNCHECK), IsItalic = true },
               new Element { Text = ConstantManager.OTHER, FontSize = 11f, ImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", patientDto?.Subject == 2 ? ConstantManager.CHECKED : ConstantManager.UNCHECK), IsItalic = true }}
            });
            _medicalBillWordDocumentService.MergeCellHorizontal(9, (0, CellMerge.Start), (1, CellMerge.Continue), (2, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 9, cellIndex = 0, leftIndent = 10, text = $"10. BHYT giá trị đến ngày {patientDto?.SocialInsurancePeriod?.Day.ToString()} tháng {patientDto?.SocialInsurancePeriod?.Month.ToString()} năm {patientDto?.SocialInsurancePeriod?.Year.ToString()},  Số thẻ BHYT: {patientDto?.InsuranceCardNumber}", fontSize = 11 });
            _medicalBillWordDocumentService.MergeCellHorizontal(10, (0, CellMerge.Start), (1, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 10, cellIndex = 0, leftIndent = 10, text = ConstantManager.FAMILY_INFO, fontSize = 11, binding = patientDto.FamilyInformation?.ToString() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 10, cellIndex = 2, leftIndent = 0, text = ConstantManager.PHONE, fontSize = 11, binding = patientDto.FamilyInformationPhone?.ToString() });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 11, cellIndex = 0, leftIndent = 10, text = ConstantManager.EXAMINATION_TIME, fontSize = 11, binding = patientDto.TimeComeExamination?.ToString("HH:mm") });
            _medicalBillWordDocumentService.MergeCellHorizontal(11, (0, CellMerge.Start), (1, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 11, cellIndex = 1, leftIndent = 0, text = ConstantManager.START_EXAMINATION_TIME, fontSize = 11, binding = patientDto.TimeStartExamination?.ToString("HH:mm") });
            _medicalBillWordDocumentService.MergeCellHorizontal(12, (0, CellMerge.Start), (1, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertTextToCellWithManyDifferentStyle(new MultiStyleTextRow() { rowIndex = 12, cellIndex = 0, LeftIndent = 10, alignment = HorizontalAlignment.Left, texts = new string[] { ConstantManager.REFERRAL_DIAGNOSIS, ConstantManager.REFERRAL_DIAGNOSIS_NOTE, $"{patientDto.DiagnosisOfReferralSite}" }, fontSizes = new float[] { 11, 11, 11 }, bolds = new bool[] { false, false, false }, italics = new bool[] { false, true, false } });
            return this;
        }
        int indexIsAddedIntermediately = 0;
        public IMedicalBilllWordDocumentBuilder MedicalExaminationInformation(PatientDto patientDto, MedicalBillInfor medicalBillInfor, List<DiagnoseDto> lstDiagnosesDto)
        {
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 13, cellIndex = 0, leftIndent = 0, text = ConstantManager.MEDICAL_EXAMINATION_INFO, fontSize = 12, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 14, cellIndex = 0, leftIndent = 10, text = ConstantManager.MODERN_MEDICINE, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 15, cellIndex = 0, leftIndent = 10, text = ConstantManager.MEDICAL_HISTORY, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 15, cellIndex = 2, leftIndent = 0, text = ConstantManager.PULSE, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 16, cellIndex = 0, leftIndent = 10, text = ConstantManager.DASH, fontSize = 11, binding = medicalBillInfor?.ReportParamData?.ReportParameterUserPersonalMedicalHistory });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 16, cellIndex = 2, leftIndent = 0, text = ConstantManager.TEMPERATURE, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 17, cellIndex = 2, leftIndent = 0, text = ConstantManager.BLOOD_PRESSURE, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 18, cellIndex = 2, leftIndent = 0, text = ConstantManager.RESPIRATORY_RATE, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 19, cellIndex = 0, leftIndent = 10, text = ConstantManager.PERSONAL_MEDICAL_HISTORY, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 19, cellIndex = 2, leftIndent = 0, text = ConstantManager.WEIGHT, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 20, cellIndex = 0, leftIndent = 10, text = ConstantManager.OWN, fontSize = 11, binding = medicalBillInfor?.ReportParamData?.ReportParameterUserPersonalMedicalHistory });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 20, cellIndex = 2, leftIndent = 0, text = ConstantManager.HEIGHT, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 21, cellIndex = 0, leftIndent = 10, text = ConstantManager.FAMILY, fontSize = 11, binding = medicalBillInfor?.ReportParamData?.ReportParameterUserFamilyMedicalHistory });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 21, cellIndex = 2, leftIndent = 0, text = ConstantManager.BMI, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 22, cellIndex = 0, leftIndent = 10, text = ConstantManager.CLINICAL_EXAMINATION, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 22, cellIndex = 2, leftIndent = 0, text = ConstantManager.SPO2, fontSize = 11, italic = true, alignment = HorizontalAlignment.Left });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 23, cellIndex = 0, leftIndent = 10, text = ConstantManager.BODY, fontSize = 11 });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 24, cellIndex = 0, leftIndent = 10, text = ConstantManager.BODY_PARTS, fontSize = 11 });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 25, cellIndex = 0, leftIndent = 10, text = ConstantManager.PRELIMINARY_DIAGNOSIS, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 26, cellIndex = 0, leftIndent = 10, text = ConstantManager.DASH, fontSize = 11 });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 27, cellIndex = 0, leftIndent = 10, text = ConstantManager.PARACLINICAL_INDICATIONS, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.MergeCellHorizontal(28, (0, CellMerge.Start), (1, CellMerge.Continue), (2, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 28, cellIndex = 0, leftIndent = 10, text = ConstantManager.TEST, fontSize = 11, binding = medicalBillInfor?.ReportParamData?.ReportParameterIndicationsParaclinicalTest });
            _medicalBillWordDocumentService.MergeCellHorizontal(29, (0, CellMerge.Start), (1, CellMerge.Continue), (2, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 29, cellIndex = 0, leftIndent = 10, text = ConstantManager.IMAGE_ANALYSATION, fontSize = 11, binding = medicalBillInfor?.ReportParamData?.ReportParameterIndicationsParaclinicalCdha });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 30, cellIndex = 0, leftIndent = 10, text = ConstantManager.SUMMARY_OF_PARACLINICAL_RESULTS, fontSize = 11, bold = true });
            _medicalBillWordDocumentService.MergeCellHorizontal(31, (0, CellMerge.Start), (1, CellMerge.Continue), (2, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 31, cellIndex = 0, leftIndent = 10, text = ConstantManager.DASH, fontSize = 11, binding = medicalBillInfor?.ReportParamData?.ReportParameterSubTest });
            _medicalBillWordDocumentService.MergeCellHorizontal(32, (0, CellMerge.Start), (1, CellMerge.Continue), (2, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 32, cellIndex = 0, leftIndent = 10, text = ConstantManager.DASH, fontSize = 11, binding = medicalBillInfor?.ReportParamData?.ReportParameterSubTest });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 33, cellIndex = 0, leftIndent = 10, text = ConstantManager.FINAL_DIAGNOSIS, fontSize = 11, bold = true });

            int diagnosesCount = lstDiagnosesDto.Count;
            for (int i = 0; i < diagnosesCount; i++)
            {
                if (i == 0) _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions() { rowIndex = 34, cellIndex = 0, leftIndent = 10, text = ConstantManager.MAIN_DISEASE, fontSize = 11 });
                _medicalBillWordDocumentService.MergeCellHorizontal(34 + i + 1 + indexIsAddedIntermediately, (0, CellMerge.Start), (1, CellMerge.Continue));
                _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions() { rowIndex = 34 + i + 1, cellIndex = 0, leftIndent = 10, fontSize = 9.5f, binding = lstDiagnosesDto[i].MainDisease });
                _medicalBillWordDocumentService.InsertTextToCellWithManyDifferentStyle(new MultiStyleTextRow() { rowIndex = 34 + i + 1, cellIndex = 2, LeftIndent = 0, texts = new string[] { ConstantManager.ICD_CODE, lstDiagnosesDto[i].ICDCode }, fontSizes = new float[] { 9.5f, 9.5f }, bolds = new bool[] { true, false }, italics = new bool[] { false, false } }); ;
            }
            for (int i = 0; i < diagnosesCount; i++)
            {
                if (i == 0) _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions() { rowIndex = 35 + diagnosesCount, cellIndex = 0, leftIndent = 10, text = ConstantManager.INCLUDING_DISEASES, fontSize = 11 }); ;
                indexIsAddedIntermediately = i + diagnosesCount + 1;
                _medicalBillWordDocumentService.MergeCellHorizontal(35 + indexIsAddedIntermediately, (0, CellMerge.Start), (1, CellMerge.Continue));
                _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions() { rowIndex = 34 + indexIsAddedIntermediately, cellIndex = 0, leftIndent = 10, fontSize = 9.5f, binding = lstDiagnosesDto[i].IncludingDiseases });
                _medicalBillWordDocumentService.InsertTextToCellWithManyDifferentStyle(new MultiStyleTextRow() { rowIndex = 35 + indexIsAddedIntermediately, cellIndex = 2, LeftIndent = 0, texts = new string[] { ConstantManager.ICD_CODE, $"{lstDiagnosesDto[i].ICDCode}" }, fontSizes = new float[] { 9.5f, 9.5f }, bolds = new bool[] { true, false }, italics = new bool[] { false, false } }); ;
            }
            return this;
        }
        public IMedicalBilllWordDocumentBuilder Solve(PatientDto patientDto, MedicalBillInfor medicalBillInfor)
        {
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 36 + indexIsAddedIntermediately, cellIndex = 0, leftIndent = 0, text = ConstantManager.SOLVE, fontSize = 12, bold = true });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 37 + indexIsAddedIntermediately, cellIndex = 2, leftIndent = 0, text = $"Hà Nội, ngày {medicalBillInfor?.ReportParamData?.ReportParameterUserStartExaminationDateTime?.Day} tháng {medicalBillInfor?.ReportParamData?.ReportParameterUserStartExaminationDateTime?.Month} năm {medicalBillInfor?.ReportParamData?.ReportParameterUserStartExaminationDateTime?.Year}", fontSize = 9.5f, italic = true, alignment = HorizontalAlignment.Center });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 38 + indexIsAddedIntermediately, cellIndex = 2, leftIndent = 0, text = ConstantManager.DOCTOR_EXAMINATION, fontSize = 11, bold = true, alignment = HorizontalAlignment.Center });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 39 + indexIsAddedIntermediately, cellIndex = 2, leftIndent = 0, text = ConstantManager.SIGNATURE_NOTE, fontSize = 9.5f, italic = true, alignment = HorizontalAlignment.Center });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 42 + indexIsAddedIntermediately, cellIndex = 2, leftIndent = 0, text = "", fontSize = 11, italic = true, alignment = HorizontalAlignment.Center, binding = medicalBillInfor?.ReportParamData?.Doctor?.Name });
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 43 + indexIsAddedIntermediately, cellIndex = 0, leftIndent = 0, text = ConstantManager.NOTE, fontSize = 12, bold = true });
            _medicalBillWordDocumentService.MergeCellHorizontal(44 + indexIsAddedIntermediately, (0, CellMerge.Start), (1, CellMerge.Continue));
            _medicalBillWordDocumentService.InsertRowToTable(new TextStyleOptions { rowIndex = 44 + indexIsAddedIntermediately, cellIndex = 0, leftIndent = 0, text = ConstantManager.PRESCRIPTION_NOTE, fontSize = 11, italic = true });
            _medicalBillWordDocumentService.GenerateBarcode(new BarcodeOptions() { rowIndex = 2, colIndex = 2, barcodeType = BarCodeType.Code39, barcodeData = medicalBillInfor.PatientId.ToString(), imagePath = ConstantManager.BARCODE_IMAGE_PATH, imageWidth = 140 });
            _medicalBillWordDocumentService.DrawBorderAround(15, 22, 2);
            return this;
        }
        public void SavePdf()
        {
            _medicalBillWordDocumentService.SavePdf();
        }

    }
}
