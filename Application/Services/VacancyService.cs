using Application.Dtos.Vacancy;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class VacancyService(IUnitOfWork unitOfWork, IValidator<VacancyRequestDto> validator, IUser user) : IVacancyService
{
    private readonly IRepository<Vacancy> _repository = unitOfWork.Repository<Vacancy>();
    private readonly IVacancyRepository _vacancyRepository = unitOfWork.Vacancies;

    public async Task<VacancyResponseDto> CreateVacancyAsync(VacancyRequestDto vacancyRequestDto)
    {
        validator.ValidateAndThrow(vacancyRequestDto);
        var mappedVacancy = vacancyRequestDto.Adapt<Vacancy>();
        mappedVacancy.HrId = user.Id;
        var createdVacancy = await _repository.CreateAsync(mappedVacancy);
        return createdVacancy.Adapt<VacancyResponseDto>();
    }

    public async Task<VacancyResponseDto> UpdateVacancyAsync(VacancyRequestDto updateVacancyRequestDto)
    {
        validator.ValidateAndThrow(updateVacancyRequestDto);
        var mappedVacancy = updateVacancyRequestDto.Adapt<Vacancy>();
        var createdVacancy = await _repository.UpdateAsync(mappedVacancy);
        return createdVacancy.Adapt<VacancyResponseDto>();
    }

    public async Task<VacancyResponseDto> GetVacancyByIdAsync(int vacancyId)
    {
        var vacancy = await _repository.GetByIdAsync(vacancyId);
        return vacancy.Adapt<VacancyResponseDto>();
    }

    public async Task<IEnumerable<VacancyResponseDto>> GetVacanciesAsync()
    {
        var vacancies = await _repository.GetAllAsync();
        return vacancies.Adapt<IEnumerable<VacancyResponseDto>>();
    }

    public async Task<IEnumerable<VacancyResponseDto>> GetAllVacanciesByCompanyIdAsync(int companyId)
    {
        var vacancies = await _vacancyRepository.GetAllVacanciesByCompanyId(companyId);
        return vacancies.Adapt<IEnumerable<VacancyResponseDto>>();
    }

    public async Task DeleteVacancyAsync(int vacancyId)
    {
        await _repository.DeleteAsync(vacancyId);
    }
}