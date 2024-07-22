using PhenikaaX.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhenikaaX.DTOs
{
    public class DiagnoseDto
    {
        public Guid DiagnoseId { get; set; }
        public Guid? PatientId { get; set; }
        public string? MainDisease { get; set; }
        public string? IncludingDiseases { get; set; }
        public string? ICDCode { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
