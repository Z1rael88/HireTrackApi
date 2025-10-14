using Application.Dtos.Resume.Technology;

namespace Application.Dtos.Requirements;

public class TechnologyRequirementDto
{
    public string Name { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public TechnologyTypeRequestDto TechnologyType { get; set; }
}