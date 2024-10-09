using Domain.Interfaces;

namespace Infrastructure.Interfaces
{
    public interface IRepository<T>
        where T : class, IBaseEntity
    {
        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}