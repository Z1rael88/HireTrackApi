using Application.Dtos;

namespace Application.Interfaces;

public interface IVacancyService
{
    Task<VacancyDto> CreateVacancyAsync(VacancyDto createVacancyDto);
    Task<VacancyDto> UpdateVacancyAsync(VacancyDto updateVacancyDto);
    Task<VacancyDto> GetVacancyByIdAsync(int vacancyId);
    Task<IEnumerable<VacancyDto>> GetVacanciesAsync();
    Task DeleteVacancyAsync(int vacancyId);
}