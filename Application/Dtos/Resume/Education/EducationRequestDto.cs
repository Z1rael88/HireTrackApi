using Domain.Enums;

namespace Application.Dtos.Resume.Education;

public class EducationRequestDto
{
    public string Title { get; set; }
    public EducationType EducationType { get; set; }
    public Degree? Degree { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Description { get; set; }
}