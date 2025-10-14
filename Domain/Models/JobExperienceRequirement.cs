namespace Domain.Models;

public class JobExperienceRequirement : BaseEntity
{
    public ICollection<TechnologyRequirement> TechnologyRequirements { get; set; }
    public int VacancyId { get; set; }
}