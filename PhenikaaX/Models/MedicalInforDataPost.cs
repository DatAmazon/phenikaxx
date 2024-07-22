namespace PhenikaaX.Models
{
    public class ReportParamData
    {
        public Doctor? Doctor { get; set; }
        public List<object>? ReportParameterDiagnoses { get; set; }
        public string? ReportParameterDepartment { get; set; }
        public string? ReportParameterDoctorName { get; set; }
        public string? ReportParameterDoctorRole { get; set; }
        public List<object>? ReportParameterOtherMethods { get; set; }
        public List<object>? ReportParameterDuringAfterSurgerys { get; set; }
        public string? ReportParameterAnesthesiologistRole { get; set; }
        public string? ReportParameterConferenceMinutes { get; set; }
        public string? ReportParameterUserAddressWork { get; set; }
        public string? ReportParameterUserAddressOther { get; set; }
        public string? ReportParameterUserPersonalMedicalHistory { get; set; }
        public string? ReportParameterUserFamilyMedicalHistory { get; set; }
        public string? ReportParameterCircuit { get; set; }
        public string? ReportParameterTemperature { get; set; }
        public string? ReportParameterBloodPressure { get; set; }
        public string? ReportParameterBreathing { get; set; }
        public string? ReportParameterWeight { get; set; }
        public string? ReportParameterReasonForVisit { get; set; }
        public string? ReportParameterPreliminaryDiagnosis { get; set; }
        public string? ReportParameterIndicationsParaclinicalTest { get; set; }
        public string? ReportParameterIndicationsParaclinicalCdha { get; set; }
        public string? ReportParameterVoteNumber { get; set; }
        public string? ReportParameterToSolve { get; set; }
        public DateTime? ReportParameterUserStartExaminationDateTime { get; set; }
        public string? ReportParameterSubTest { get; set; }
        public ConferenceMinutes? ConferenceMinutes { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class Doctor
    {
        public string? Name { get; set; }
        public string? Label { get; set; }
        public string? Value { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
    }

    public class ConferenceMinutes
    {
        public string? Value { get; set; }
        public string? Label { get; set; }
    }

    public class MedicalBillInfor
    {
        public string? ReportTypeCode { get; set; }
        public ReportParamData? ReportParamData { get; set; }
        public string? PatientReceptionId { get; set; }
        public string? PatientDesignateServiceId { get; set; }
        public string? BasicInformationPatientId { get; set; }
        public Guid? PatientId { get; set; }

    }

    public class MedicalInforDataPost
    {
        public string? ReportTypeCode { get; set; }
        public string? ReportParamData { get; set; }
        public string? PatientReceptionId { get; set; }
        public string? PatientDesignateServiceId { get; set; }
        public string? BasicInformationPatientId { get; set; }
        //[JsonPropertyName("")]
        public Guid? PatientId { get; set; }
    }
}
