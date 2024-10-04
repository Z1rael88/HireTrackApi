using Application.Dtos;

namespace Application.Interfaces;

public interface IVacancyService
{
    Task<VacancyDto> CreateVacancyAsync(VacancyDto createVacancyDto);
    Task<VacancyDto> UpdateVacancyAsync(VacancyDto updateVacancyDto);
    Task<VacancyDto> GetVacancyByIdAsync(Guid vacancyId);
    Task<IEnumerable<VacancyDto>> GetVacanciesAsync();
    Task DeleteVacancyAsync(Guid vacancyId);
}