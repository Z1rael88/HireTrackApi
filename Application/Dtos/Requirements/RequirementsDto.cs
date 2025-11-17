namespace Application.Dtos.Requirements;

public class RequirementsDto
{
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelRequirementDto> LanguageLevels { get; set; } 
    public JobExperienceRequirementDto JobExperience { get; set; } 
    public EducationRequirementDto Education { get; set; }
}