using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IVacancyRepository 
{
    Task<IEnumerable<Vacancy>> GetAllVacanciesByCompanyId(int companyId);
    Task<Vacancy> GetByIdAsync(int vacancyId);
}