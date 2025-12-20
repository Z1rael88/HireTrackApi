using Domain.Enums;

namespace Domain.Models;

public class LanguageLevel : BaseEntity
{
    public required Language Language { get; set; }
    public required Level Level { get; set; }
    public int ResumeId { get; set; }
}