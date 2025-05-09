using Domain.Enums;

namespace Domain.Models;

public class LanguageLevel : BaseEntity
{
    public required Language Language { get; set; }
    public Enums.LanguageLevel Level { get; set; }
    public int ResumeId { get; set; }
}