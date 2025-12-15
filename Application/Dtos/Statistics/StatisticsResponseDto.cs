namespace Application.Dtos.Statistics;

public class StatisticsResponseDto
{
    public TotalStatistics TotalStatistics { get; set; }
    public EducationStatistics EducationStatistics { get; set; }
    public ExperienceStatistics ExperienceStatistics { get; set; }
    public LanguageLevelStatistics LanguageLevelStatistics { get; set; }
}