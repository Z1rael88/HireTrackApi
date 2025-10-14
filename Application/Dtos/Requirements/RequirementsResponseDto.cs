namespace Application.Dtos.Requirements;

public class RequirementsResponseDto
{
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelRequirementDto> LanguageLevels { get; set; } 
    public ICollection<JobExperienceRequirementResponseDto> JobExperiences { get; set; } 
    public ICollection<EducationRequirementDto> Educations { get; set; }
}