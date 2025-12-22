using Domain.Models;

namespace Application.Interfaces;

public interface IStatisticsRepository
{
    Task<IEnumerable<Statistics>> GetAllStatisticsByVacancyId(int vacancyId);
}