using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StatisticsRepository(ApplicationDbContext dbContext) : IStatisticsRepository
{
    public async Task<IEnumerable<Statistics>> GetAllStatisticsByVacancyIdAsync(int vacancyId)
    {
        return await dbContext.Statistics.Where(x => x.VacancyId == vacancyId).ToListAsync();
    }
    public async Task<Statistics> GetStatisticsByResumeIdAsync(int resumeId)
    {
        var result = await dbContext.Statistics.FirstOrDefaultAsync(x => x.ResumeId == resumeId);
        if (result is null)
            throw new NotFoundException("No statistics found for this resume");
        return result;
    }
}