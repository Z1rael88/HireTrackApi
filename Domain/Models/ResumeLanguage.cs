namespace Domain.Models;

public class ResumeLanguage
{
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}