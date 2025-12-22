namespace Domain.Models;

public class Technology : BaseEntity
{
    public int YearsOfExperience { get; set; }
    public TechnologyType TechnologyType { get; set; }
    public int TechnologyTypeId { get; set; }
    public int JobExperienceId { get; set; }
}