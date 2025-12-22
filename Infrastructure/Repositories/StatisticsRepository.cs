using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StatisticsRepository(ApplicationDbContext dbContext) : IStatisticsRepository
{
    public async Task<IEnumerable<Statistics>> GetAllStatisticsByVacancyId(int vacancyId)
    {
        return await dbContext.Statistics.Where(x => x.VacancyId == vacancyId).ToListAsync();
    }
}