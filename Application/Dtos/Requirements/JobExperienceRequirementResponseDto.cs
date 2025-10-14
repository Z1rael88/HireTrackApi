namespace Application.Dtos.Requirements;

public class JobExperienceRequirementResponseDto
{
    public ICollection<TechnologyRequirementResponseDto> TechnologyRequirements { get; set; }
}