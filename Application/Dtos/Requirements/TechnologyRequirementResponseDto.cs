namespace Application.Dtos.Requirements;

public class TechnologyRequirementResponseDto
{
    public string Name { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public TechnologyTypeDto TechnologyTypeDto { get; set; }
}