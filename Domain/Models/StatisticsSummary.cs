using Microsoft.EntityFrameworkCore;

namespace Domain.Models;
[Owned]
public class StatisticsSummary
{
    public string TotalSummary { get; set; }
    public string EducationSummary { get; set; }
    public string ExperienceSummary { get; set; }
    public string LanguageLevelSummary { get; set; }

}