using Application.Dtos.Requirements;
using Application.Dtos.Resume;
using Domain.Enums;

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
    public AddressDto Address { get; set; }
    public string Responsibilities { get; set; }
    public RequirementsResponseDto Requirements { get; set; }
}