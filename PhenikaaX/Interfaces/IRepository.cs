namespace PhenikaaX.Intefaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
