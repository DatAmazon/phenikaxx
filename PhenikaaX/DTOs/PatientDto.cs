namespace PhenikaaX.DTOs
{
    public class PatientDto
    {
        public Guid PatientId { get; set; }
        public string? Username { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Job { get; set; }
        public string? Ethnic { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public string? Workplace { get; set; }
        public string? Phone { get; set; }
        public int? Type { get; set; }
        public int? Subject { get; set; }
        public DateTime? SocialInsurancePeriod { get; set; }
        public string? InsuranceCardNumber { get; set; }
        public string? FamilyInformation { get; set; }
        public string? FamilyInformationPhone { get; set; }
        public DateTime? TimeComeExamination { get; set; }
        public DateTime? TimeStartExamination { get; set; }
        public string? DiagnosisOfReferralSite { get; set; }
        public string? OrderNumber { get; set; }
    }
}
