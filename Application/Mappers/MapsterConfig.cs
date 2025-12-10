using Application.Dtos;
using Application.Dtos.Requirements;
using Application.Dtos.Resume;
using Application.Dtos.Resume.Technology;
using Application.Dtos.Statistics;
using Application.Dtos.User;
using Application.Dtos.Vacancy;
using Domain.Models;
using Mapster;

namespace Application.Mappers;

public static class MapsterConfig
{
    public static void VacancyMappings()
    {
        TypeAdapterConfig<VacancyRequestDto, Vacancy>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Salary, src => src.Salary)
            .Map(dest => dest.AddDate, src => src.AddDate)
            .Map(dest => dest.EndDate, src => src.EndDate)
            .Map(dest => dest.EducationsRequirement, src => src.Requirements.Education)
            .Map(dest => dest.LanguageLevelRequirements, src => src.Requirements.LanguageLevels)
            .Map(dest => dest.JobExperienceRequirement, src => src.Requirements.JobExperience)
            .Map(dest => dest.YearsOfExperience, src => src.Requirements.YearsOfExperience);
        TypeAdapterConfig<LanguageLevelRequirement, LanguageLevelRequirementDto>
            .NewConfig()
            .Map(dest => dest.Language, src => src.Language)
            .Map(dest => dest.Level, src => src.Level);
        TypeAdapterConfig<JobExperienceRequirement, JobExperienceRequirementDto>
            .NewConfig()
            .Map(dest => dest.TechnologyRequirements, src => src.TechnologyRequirements);
        
        TypeAdapterConfig<EducationRequirement, EducationRequirementDto>
            .NewConfig()
            .Map(dest => dest.Degree, src => src.Degree)
            .Map(dest => dest.EducationType, src => src.EducationType);

        TypeAdapterConfig<TechnologyRequirement, TechnologyRequirementDto>
            .NewConfig() 
            .Map(dest => dest.TechnologyTypeId, src => src.TechnologyTypeId)
            .Map(dest => dest.YearsOfExperience, src => src.YearsOfExperience);
        TypeAdapterConfig<Vacancy, VacancyResponseDto>
            .NewConfig()
            .Map(dest => dest.Requirements, src => new RequirementsResponseDto()
            {
                LanguageLevels = src.LanguageLevelRequirements.Adapt<ICollection<LanguageLevelRequirementDto>>(),
                Education = src.EducationsRequirement.Adapt<EducationRequirementDto>(),
                JobExperience = src.JobExperienceRequirement.Adapt<JobExperienceRequirementResponseDto>(),
                YearsOfExperience = src.YearsOfExperience,
            });
           
        TypeAdapterConfig<TechnologyType, TechnologyTypeDto>
            .NewConfig();
        TypeAdapterConfig<TechnologyRequirement, TechnologyRequirementResponseDto>
            .NewConfig()
            .Map(dest => dest.TechnologyTypeDto, src =>src.TechnologyType);

    }

    public static void UserMappings()
    {
        TypeAdapterConfig<RegisterUserDto, User>
            .NewConfig()
            .Map(x => x.Firstname, y => y.FirstName)
            .Map(x => x.Lastname, y => y.LastName)
            .Map(x => x.Age, y => y.Age)
            .Map(x => x.Email, y => y.Email)
            .Map(x => x.UserName, y => y.Email)
            .Map(x => x.Firstname, y => y.FirstName);
        TypeAdapterConfig<User, UserResponseDto>
            .NewConfig()
            .Map(x => x.Username, y => y.UserName)
            .Map(x => x.Id, y => y.Id);
        TypeAdapterConfig<UserWithRole, UserResponseDto>
            .NewConfig()
            .Map(x => x.Id, y => y.User.Id)
            .Map(x => x.Firstname, y => y.User.Firstname)
            .Map(x => x.Lastname, y => y.User.Lastname)
            .Map(x => x.Username, y => y.User.UserName)
            .Map(x => x.Email, y => y.User.Email);
        TypeAdapterConfig<Statistic, StatisticResponseDto>
            .NewConfig()
            .Map(dest => dest.EducationStatistics.EducationSummary, src => src.EducationSummary)
            .Map(dest => dest.EducationStatistics.EducationMatchPercent, src => src.EducationMatchPercent);
    }

    public static void ResumeMappings()
    {
        TypeAdapterConfig<ResumeRequestDto, Resume>
            .NewConfig()
            .Map(x => x.Candidate, y => y.Candidate)
            .Map(x => x.YearsOfExperience, y => y.YearsOfExperience)
            .Map(x => x.Educations, y => y.Educations)
            .Map(x => x.JobExperiences, y => y.JobExperiences)
            .Map(x => x.LanguageLevels, y => y.LanguageLevels);
        TypeAdapterConfig<Resume, ResumeResponseDto>
            .NewConfig()
            .Map(x => x.Candidate, y => y.Candidate)
            .Map(x => x.YearsOfExperience, y => y.YearsOfExperience)
            .Map(x => x.Educations, y => y.Educations)
            .Map(x => x.JobExperiences, y => y.JobExperiences);
    }
}