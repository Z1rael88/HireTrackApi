using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private bool _disposed;
        private IVacancyRepository _vacancyRepository;
        private IResumeRepository _resumeRepository;
        private ICandidateRepository _candidateRepository;
        private IStatisticsRepository _statisticsRepository;


        public IRepository<T> Repository<T>() where T : class, IBaseEntity
        {
            return new Repository<T>(_dbContext); 
        }

        public IVacancyRepository Vacancies => _vacancyRepository ??= new VacancyRepository(_dbContext);
        public IResumeRepository Resumes => _resumeRepository ??= new ResumeRepository(_dbContext);
        public ICandidateRepository Candidates => _candidateRepository ??= new CandidateRepository(_dbContext);
        public IStatisticsRepository Statistics => _statisticsRepository ??= new StatisticsRepository(_dbContext);


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose(); 
                }
            }
            _disposed = true;
        }
    }
}