using Domain.Enums;

namespace Application.Dtos.Requirements;

public class EducationRequirementDto
{
    public EducationType EducationType { get; set; }
    public Degree? Degree { get; set; }
}