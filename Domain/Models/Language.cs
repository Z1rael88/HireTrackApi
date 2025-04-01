using Domain.Enums;

namespace Domain.Models;

public class Language : BaseEntity
{
    public string LanguageName { get; set; } = string.Empty;
    public LanguageLevel LanguageLevel { get; set; } 
    public List<ResumeLanguage> ResumeLanguages { get; set; } = new();
}