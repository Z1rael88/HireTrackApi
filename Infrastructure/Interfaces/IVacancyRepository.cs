using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IVacancyRepository 
{
    Task<IEnumerable<Vacancy>> GetAllVacanciesByCompanyIdAsync(int companyId);
    Task<Vacancy> GetByIdAsync(int vacancyId);
    Task<IEnumerable<Vacancy>> GetAllVacanciesByIdsAsync(List<int> vacancyIds);
    Task<ResumeStatus> GetResumeStatusByResumeIdAsync(int resumeId);
}