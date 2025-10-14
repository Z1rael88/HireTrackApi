using Domain.Enums;

namespace Domain.Models;

public class LanguageLevelRequirement : BaseEntity
{
    public required Language Language { get; set; }
    public required Enums.LanguageLevel Level { get; set; }
    public int VacancyId { get; set; }
}