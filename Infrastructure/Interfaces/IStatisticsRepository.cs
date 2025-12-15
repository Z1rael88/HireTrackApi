using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IStatisticsRepository
{
    Task<IEnumerable<Statistics>> GetAllStatisticsByVacancyId(int vacancyId);
}