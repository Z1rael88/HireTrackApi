using Domain.Interfaces;
using Infrastructure.Interfaces;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class, IBaseEntity;
    IVacancyRepository Vacancies { get; }
    IResumeRepository Resumes { get; }
    ICandidateRepository Candidates { get; }
    IStatisticsRepository Statistics { get; }

    Task SaveChangesAsync();
}