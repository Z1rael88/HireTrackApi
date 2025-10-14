namespace Application.Dtos.Requirements;

public class JobExperienceRequirementDto
{
    public ICollection<TechnologyRequirementDto> TechnologyRequirements { get; set; }
}