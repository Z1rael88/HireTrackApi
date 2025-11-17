namespace Domain.Models;

public class Statistic : BaseEntity
{
    public double TotalMatchPercent { get; set; }
    public string Summary { get; set; } = string.Empty;
    public double LanguageMatchPercent { get; set; }
    public string LanguageSummary { get; set; } = string.Empty;
    public double EducationMatchPercent { get; set; }
    public string EducationSummary { get; set; } = string.Empty;
    public double ExperienceMatchPercent { get; set; }
    public string ExperienceSummary { get; set; } = string.Empty;
    
    public int ResumeId { get; set; }
    public int VacancyId { get; set; }
}