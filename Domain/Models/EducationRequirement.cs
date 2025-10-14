using Domain.Enums;

namespace Domain.Models;

public class EducationRequirement : BaseEntity
{
    public EducationType EducationType { get; set; }
    public Degree? Degree { get; set; }
    public int VacancyId { get; set; }
}
