using PhenikaaX.Entities;
using PhenikaaX.Intefaces;
using PhenikaaX.IServices;
using PhenikaaX.Repository;

namespace PhenikaaX.IService
{
    public interface IUnitOfWork
    {
        IRepository<Patient> Patients { get; }
        IRepository<Diagnose> Diagnoses { get; }
        Task<int> SaveChangesAsync();
    }
}
