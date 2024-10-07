using Domain.Interfaces;

namespace Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class, IBaseEntity;

    Task SaveChangesAsync();
}