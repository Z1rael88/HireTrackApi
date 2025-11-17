namespace Application.Dtos.Statistics;

public class StatisticResponseDto
{
    public double TotalMatchPercent { get; set; }
    public string Summary { get; set; } = string.Empty;
    public double LanguageMatchPercent { get; set; }
    public string LanguageSummary { get; set; } = string.Empty;
    public EducationStatistics EducationStatistics { get; set; }
    public ExperienceStatistics ExperienceStatistics { get; set; }
}