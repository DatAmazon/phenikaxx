using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhenikaaX.Entities;
[Table("patient")]
public class Patient
{
    [Key]
    [Column("patient_id")]
    public Guid PatientId { get; set; }

    [StringLength(100)]
    public string? Username { get; set; }
    public DateTime? Birthday { get; set; }
    public int? Age { get; set; }

    [StringLength(20)]
    public string? Gender { get; set; }

    [StringLength(50)]
    public string? Job { get; set; }

    [StringLength(50)]
    public string? Ethnic { get; set; }

    [StringLength(50)]
    public string? Nationality { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(200)]
    public string? Workplace { get; set; }

    [StringLength(12)]

    public string? Phone { get; set; }
    public int? Type { get; set; }
    public int? Subject { get; set; }

    [Column("social_insurance_period", TypeName = "datetime")]
    public DateTime? SocialInsurancePeriod { get; set; }

    [Column("insurance_card_number")]
    public string? InsuranceCardNumber { get; set; }

    [Column("family_information")]
    public string? FamilyInformation { get; set; }

    [Column("family_information_phone")]
    public string? FamilyInformationPhone { get; set; }

    [Column("time_come_examination", TypeName = "datetime")]
    public DateTime? TimeComeExamination { get; set; }

    [Column("time_start_examination", TypeName = "datetime")]
    public DateTime? TimeStartExamination { get; set; }

    [Column("diagnosis_of_referral_site")]
    public string? DiagnosisOfReferralSite { get; set; }

    [Column("order_number")]
    public string? OrderNumber { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();
}
