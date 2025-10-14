namespace Application.Dtos.Requirements;

public class RequirementsDto
{
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelRequirementDto> LanguageLevels { get; set; } 
    public ICollection<JobExperienceRequirementDto> JobExperiences { get; set; } 
    public ICollection<EducationRequirementDto> Educations { get; set; }
}