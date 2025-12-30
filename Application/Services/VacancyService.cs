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
    IVacancyRepository vacancyRepository,
    IResumeRepository resumeRepository,
    IValidator<VacancyRequestDto> validator,
    IUser user,
    UserManager<User> userManager) : IVacancyService
{
    public async Task<VacancyResponseDto> CreateVacancyAsync(VacancyRequestDto vacancyRequestDto)
    {
        validator.ValidateAndThrow(vacancyRequestDto);
        var mappedVacancy = vacancyRequestDto.Adapt<Vacancy>();
        mappedVacancy.HrId = user.Id;
        var createdVacancy = await vacancyRepository.CreateAsync(mappedVacancy);
        return createdVacancy.Adapt<VacancyResponseDto>();
    }

    public async Task<VacancyResponseDto> GetVacancyByIdAsync(int vacancyId)
    {
        var vacancy = await vacancyRepository.GetByIdIncludedAsync(vacancyId);
        return vacancy.Adapt<VacancyResponseDto>();
    }

    public async Task<IEnumerable<VacancyResponseDto>> GetVacanciesAsync()
    {
        var vacancies = await vacancyRepository.GetAllAsync();
        return vacancies.Adapt<IEnumerable<VacancyResponseDto>>();
    }

    public async Task<IEnumerable<VacancyResponseDto>> GetAllVacanciesByCompanyIdAsync(int companyId)
    {
        var vacancies = await vacancyRepository.GetAllVacanciesByCompanyIdAsync(companyId);
        return vacancies.Adapt<IEnumerable<VacancyResponseDto>>();
    }

    public async Task DeleteVacancyAsync(int vacancyId)
    {
        await vacancyRepository.DeleteAsync(vacancyId);
    }

    public async Task<IEnumerable<VacancyWithStatusDto>?> GetVacanciesByUserIdAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString())
                   ?? throw new NotFoundException("User with such id not found");

        var resume = await resumeRepository.GetResumeByCandidateEmail(user.Email!);
        if (resume is null)
        {
            throw new NotFoundException("Resume for user with that email is not found");
        }
        var vacancyResumes = await resumeRepository.GetAllVacancyResumesByResumeIdAsync(resume.Id);
        if (vacancyResumes is null)
        {
            return null;
        }
        var vacancies = vacancyResumes.Select(x => x.Vacancy);
        var statusByVacancyId = vacancyResumes
            .ToDictionary(x => x.VacancyId, x => x.Status);

        return vacancies.Select(vacancy => new VacancyWithStatusDto
        {
            Id = vacancy.Id,
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

    public async Task<IEnumerable<VacancyResponseDto>> GetByHrIdAsync(int hrId)
    {
        var vacancy = await vacancyRepository.GetByHrIdAsync(hrId);
        return vacancy.Adapt<IEnumerable<VacancyResponseDto>>();
    }

    public async Task UpdateVacancyAsync(VacancyRequestDto vacancy, int vacancyId)
    {
        await vacancyRepository.UpdateVacancyAsync(vacancy.Adapt<Vacancy>(), vacancyId);
    }
}