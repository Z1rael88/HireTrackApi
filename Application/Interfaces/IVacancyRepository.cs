using Domain.Enums;
using Domain.Models;

namespace Application.Interfaces;

public interface IVacancyRepository 
{
    Task<IEnumerable<Vacancy>> GetAllVacanciesByCompanyIdAsync(int companyId);
    Task<Vacancy> GetByIdAsync(int vacancyId);
    Task<IEnumerable<Vacancy>> GetAllVacanciesByIdsAsync(List<int> vacancyIds);
    Task<ResumeStatus> GetResumeStatusByResumeIdAsync(int resumeId);
    Task<IEnumerable<Vacancy>>  GetByHrIdAsync(int hrId);
    Task UpdateVacancyAsync(Vacancy vacancy, int vacancyId);
}