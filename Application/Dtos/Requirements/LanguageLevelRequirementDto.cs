using Domain.Enums;

namespace Application.Dtos.Requirements;

public class LanguageLevelRequirementDto
{
    public required Language Language { get; set; }
    public required Level Level { get; set; }
}