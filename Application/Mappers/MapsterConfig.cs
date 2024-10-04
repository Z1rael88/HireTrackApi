using Application.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Mappers;

public static class MapsterConfig
{
    public static void VacancyMappings()
    {
        TypeAdapterConfig<VacancyDto, Vacancy>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Salary, src => src.Salary)
            .Map(dest => dest.AddDate, src => src.AddDate)
            .Map(dest => dest.EndDate, src => src.EndDate);
    }
}