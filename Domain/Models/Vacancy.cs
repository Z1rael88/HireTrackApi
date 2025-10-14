namespace Domain.Models;

public class Vacancy : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }= string.Empty;
    public decimal Salary { get; set; }
    public DateOnly AddDate { get; set; }
    public DateOnly EndDate { get; set; }
    public User Hr { get; set; }
    public int HrId { get; set; }
    public Company Company { get; set; }
    public required int CompanyId { get; set; }
    public int YearsOfExperience { get; set; }
    public ICollection<LanguageLevelRequirement> LanguageLevelRequirements { get; set; } 
    public ICollection<JobExperienceRequirement> JobExperienceRequirements { get; set; } 
    public ICollection<EducationRequirement> EducationsRequirements { get; set; }
    public ICollection<VacancyResume> VacancyResumes { get; set; }
}