using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhenikaaX.Entities;
[Table("Diagnose")]
public class Diagnose
{
    [Key]
    [Column("diagnose_id")]
    public Guid DiagnoseId { get; set; }

    [Column("patient_id")]
    public Guid? PatientId { get; set; }

    [Column("main_disease")]
    [StringLength(200)]
    public string? MainDisease { get; set; }

    [Column("including_diseases")]
    [StringLength(200)]
    public string? IncludingDiseases { get; set; }

    [Column("ICD_code")]
    [StringLength(50)]
    public string? ICDCode { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Diagnoses")]
    public virtual Patient? Patient { get; set; }
}
