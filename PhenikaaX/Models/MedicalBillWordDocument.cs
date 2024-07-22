using PhenikaaX.Intefaces;
using Spire.Doc;

namespace PhenikaaX.Models
{
    public class MedicalBillWordDocument 
    {
        public Table? Title { get; set; }
        public Table? Administrative { get; set; }
        public Table? MedicalExaminationInformation { get; set; }
        public Table? Solve { get; set; }

    }
}
