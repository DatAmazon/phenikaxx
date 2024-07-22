using Microsoft.EntityFrameworkCore;
using PhenikaaX.Entities;
using PhenikaaX.Intefaces;
using System;

namespace PhenikaaX.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PhenikaaXContext _context;

        public Repository(PhenikaaXContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }
    }
}
