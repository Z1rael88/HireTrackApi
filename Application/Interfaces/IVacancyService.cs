using Application.Dtos.Vacancy;

namespace Application.Interfaces;

public interface IVacancyService
{
    Task<VacancyResponseDto> CreateVacancyAsync(VacancyRequestDto createVacancyRequestDto);
    Task<VacancyResponseDto> UpdateVacancyAsync(VacancyRequestDto updateVacancyRequestDto);
    Task<VacancyResponseDto> GetVacancyByIdAsync(int vacancyId);
    Task<IEnumerable<VacancyResponseDto>> GetVacanciesAsync();
    Task<IEnumerable<VacancyResponseDto>> GetAllVacanciesByCompanyIdAsync(int companyId);
    Task DeleteVacancyAsync(int vacancyId);
}