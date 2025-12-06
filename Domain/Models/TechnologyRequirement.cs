namespace Domain.Models;

public class TechnologyRequirement : BaseEntity
{
    public int YearsOfExperience { get; set; }
    public TechnologyType TechnologyType { get; set; }
    public int TechnologyTypeId { get; set; }
    public int JobExperienceRequirementId { get; set; }
    public JobExperienceRequirement JobExperienceRequirement { get; set; } = null!;
}