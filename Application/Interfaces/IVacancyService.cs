using Application.Dtos.Vacancy;

namespace Application.Interfaces;

public interface IVacancyService
{
    Task<VacancyResponseDto> CreateVacancyAsync(VacancyRequestDto createVacancyRequestDto);
    Task<VacancyResponseDto> GetVacancyByIdAsync(int vacancyId);
    Task<IEnumerable<VacancyResponseDto>> GetVacanciesAsync();
    Task<IEnumerable<VacancyResponseDto>> GetAllVacanciesByCompanyIdAsync(int companyId);
    Task DeleteVacancyAsync(int vacancyId);
    Task<IEnumerable<VacancyWithStatusDto>?> GetVacanciesByUserIdAsync(int userId);
    Task<IEnumerable<VacancyResponseDto>>  GetByHrIdAsync(int hrId);
    Task UpdateVacancyAsync(VacancyRequestDto vacancy, int vacancyId);
}