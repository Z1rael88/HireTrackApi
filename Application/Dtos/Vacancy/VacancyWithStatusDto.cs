using Domain.Enums;

namespace Application.Dtos.Vacancy;

public class VacancyWithStatusDto : VacancyResponseDto
{
    public ResumeStatus Status { get; set; }
}