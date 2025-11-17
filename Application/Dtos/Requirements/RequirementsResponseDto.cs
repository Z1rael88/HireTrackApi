namespace Application.Dtos.Requirements;

public class RequirementsResponseDto
{
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelRequirementDto> LanguageLevels { get; set; } 
    public JobExperienceRequirementResponseDto JobExperience { get; set; } 
    public EducationRequirementDto Education { get; set; }
}