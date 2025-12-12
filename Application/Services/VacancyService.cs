using Application.Dtos.Requirements;
using Application.Dtos.Resume;
using Application.Dtos.Vacancy;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class VacancyService(
    IUnitOfWork unitOfWork,
    IValidator<VacancyRequestDto> validator,
    IUser user,
    UserManager<User> userManager) : IVacancyService
{
    private readonly IRepository<Vacancy> _repository = unitOfWork.Repository<Vacancy>();
    private readonly IVacancyRepository _vacancyRepository = unitOfWork.Vacancies;
    private readonly IResumeRepository _resumeRepository = unitOfWork.Resumes;

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
        var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);
        return vacancy.Adapt<VacancyResponseDto>();
    }

    public async Task<IEnumerable<VacancyResponseDto>> GetVacanciesAsync()
    {
        var vacancies = await _repository.GetAllAsync();
        return vacancies.Adapt<IEnumerable<VacancyResponseDto>>();
    }

    public async Task<IEnumerable<VacancyResponseDto>> GetAllVacanciesByCompanyIdAsync(int companyId)
    {
        var vacancies = await _vacancyRepository.GetAllVacanciesByCompanyIdAsync(companyId);
        return vacancies.Adapt<IEnumerable<VacancyResponseDto>>();
    }

    public async Task DeleteVacancyAsync(int vacancyId)
    {
        await _repository.DeleteAsync(vacancyId);
    }

    public async Task<IEnumerable<VacancyWithStatusDto>> GetVacanciesByUserIdAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString())
                   ?? throw new NotFoundException("User with such id not found");

        var resume = await _resumeRepository.GetResumeByCandidateEmail(user.Email!);
        var vacancyResumes = await _resumeRepository.GetAllVacancyResumesByResumeIdAsync(resume.Id);
        var vacancies = vacancyResumes.Select(x => x.Vacancy);
        var statusByVacancyId = vacancyResumes
            .ToDictionary(x => x.VacancyId, x => x.Status);

        return vacancies.Select(vacancy => new VacancyWithStatusDto
        {
            HrId = vacancy.HrId,
            CompanyId = vacancy.CompanyId,
            Name = vacancy.Name,
            Description = vacancy.Description,
            Salary = vacancy.Salary,
            AddDate = vacancy.AddDate,
            EndDate = vacancy.EndDate,
            WorkType = vacancy.WorkType,
            Address = vacancy.Address.Adapt<AddressDto>(),
            Responsibilities = vacancy.Responsibilities,
            Requirements = new RequirementsResponseDto
            {
                YearsOfExperience = vacancy.YearsOfExperience,
                LanguageLevels = vacancy.LanguageLevelRequirements.Adapt<ICollection<LanguageLevelRequirementDto>>(),
                JobExperience = vacancy.JobExperienceRequirement.Adapt<JobExperienceRequirementResponseDto>(),
                Education = vacancy.EducationsRequirement.Adapt<EducationRequirementDto>()
            },
            Status = statusByVacancyId[vacancy.Id] 
        });
    }

}