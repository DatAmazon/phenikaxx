using PhenikaaX.Entities;
using PhenikaaX.Models;

namespace PhenikaaX.IServices
{
    public interface IMedicalBillService
    {
        Task<string> CreateMedicalBillToPdf(MedicalBillInfor medicalBillInfor);
    }   
}
