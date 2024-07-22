using PhenikaaX.Entities;
using PhenikaaX.Intefaces;
using PhenikaaX.IService;
using PhenikaaX.IServices;

namespace PhenikaaX.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhenikaaXContext _phenikaaXContext;

        public UnitOfWork(PhenikaaXContext context)
        {
            _phenikaaXContext = context;
            Patients = new Repository<Patient>(_phenikaaXContext);
            Diagnoses = new Repository<Diagnose>(_phenikaaXContext);
        }

        public IRepository<Patient> Patients { get; private set; }
        public IRepository<Diagnose> Diagnoses { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _phenikaaXContext.SaveChangesAsync();
        }
    }
}
