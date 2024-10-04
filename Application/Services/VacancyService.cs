using Application.Dtos;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class VacancyService(IUnitOfWork unitOfWork) : IVacancyService
{
    private readonly IRepository<Vacancy> _repository = unitOfWork.Repository<Vacancy>();

    public async Task<VacancyDto> CreateVacancyAsync(VacancyDto vacancyDto)
    {
        var mappedVacancy = vacancyDto.Adapt<Vacancy>();
        var createdVacancy = await _repository.CreateAsync(mappedVacancy);
        return createdVacancy.Adapt<VacancyDto>();
    }

    public async Task<VacancyDto> UpdateVacancyAsync(VacancyDto updateVacancyDto)
    {
        var mappedVacancy = updateVacancyDto.Adapt<Vacancy>();
        var createdVacancy = await _repository.UpdateAsync(mappedVacancy);
        return createdVacancy.Adapt<VacancyDto>();
    }

    public async Task<VacancyDto> GetVacancyByIdAsync(Guid vacancyId)
    {
        var vacancy = await _repository.GetByIdAsync(vacancyId);
        return vacancy.Adapt<VacancyDto>();
    }

    public async Task<IEnumerable<VacancyDto>> GetVacanciesAsync()
    {
        var vacancies = await _repository.GetAllAsync();
        return vacancies.Adapt<IEnumerable<VacancyDto>>();
    }

    public async Task DeleteVacancyAsync(Guid vacancyId)
    {
        await _repository.DeleteAsync(vacancyId);
    }
}