using Application.Dtos.Requirements;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Dtos.Vacancy;

public class VacancyResponseDto
{
    public int Id { get; set; }
    public int HrId { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }= string.Empty;
    public decimal Salary { get; set; }
    public DateOnly AddDate { get; set; }
    public DateOnly EndDate { get; set; }
    public WorkType WorkType { get; set; }
    public RequirementsResponseDto Requirements { get; set; }
}