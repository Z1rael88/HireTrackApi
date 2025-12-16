using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StatisticsRepository(IApplicationDbContext dbContext) : IStatisticsRepository
{
    public async Task<IEnumerable<Statistics>> GetAllStatisticsByVacancyId(int vacancyId)
    {
        return await dbContext.Statistics.Where(x => x.VacancyId == vacancyId).OrderByDescending(x=>x.TotalMatchPercent).ToListAsync();
    }
}