using Domain.Models;

namespace Application.Interfaces;

public interface IStatisticsRepository
{
    Task<IEnumerable<Statistics>> GetAllStatisticsByVacancyIdAsync(int vacancyId);
    Task<Statistics> GetStatisticsByResumeIdAsync(int resumeId);
}