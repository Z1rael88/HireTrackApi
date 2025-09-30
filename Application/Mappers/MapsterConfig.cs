using Application.Dtos;
using Application.Dtos.Resume;
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
            .Map(dest => dest.EndDate, src => src.EndDate);
    }

    public static void UserMappings()
    {
        TypeAdapterConfig<RegisterUserDto, User>
            .NewConfig()
            .Map(x => x.Firstname, y => y.FirstName)
            .Map(x => x.Lastname, y => y.LastName)
            .Map(x => x.Age, y => y.Age)
            .Map(x => x.UserName, y => y.Username)
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
    }

    public static void ResumeMappings()
    {
        TypeAdapterConfig<ResumeRequestDto, Resume>
            .NewConfig()
            .Map(x => x.Candidate,y => y.Candidate)
            .Map(x => x.YearsOfExperience, y => y.YearsOfExperience)
            .Map(x => x.Educations, y => y.Educations);
        TypeAdapterConfig<Resume, ResumeResponseDto>
            .NewConfig()
            .Map(x => x.Candidate, y => y.Candidate)
            .Map(x => x.YearsOfExperience, y => y.YearsOfExperience)
            .Map(x => x.Educations, y => y.Educations)
            .Map(x => x.JobExperiences, y => y.JobExperiences);
    }
}