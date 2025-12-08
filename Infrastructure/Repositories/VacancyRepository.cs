using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VacancyRepository(IApplicationDbContext dbContext) : IVacancyRepository
{
    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCompanyId(int companyId)
    {
        return await dbContext.Vacancies.Where(x => x.CompanyId == companyId)
            .Include(x => x.EducationsRequirement)
            .Include(x => x.JobExperienceRequirement)
            .ThenInclude(x=>x.TechnologyRequirements)
            .ThenInclude(x=>x.TechnologyType)
            .Include(x => x.LanguageLevelRequirements).ToListAsync();
    }

    public async Task<Vacancy> GetByIdAsync(int vacancyId)
    {
        var result = await dbContext.Vacancies
            .Where(x => x.Id == vacancyId)
            .Include(x => x.EducationsRequirement)
            .Include(x => x.JobExperienceRequirement)
            .ThenInclude(x => x.TechnologyRequirements)
            .ThenInclude(x => x.TechnologyType)
            .Include(x => x.LanguageLevelRequirements).FirstOrDefaultAsync();
        if (result is null)
        {
            throw new NotFoundException($"Entity with id {vacancyId} not found");
        }
        return result;
    }

    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByIds(List<int> vacancyIds)
    {
        return await dbContext.Vacancies
            .Where(v => vacancyIds.Contains(v.Id))
            .Include(v => v.EducationsRequirement)
            .Include(v => v.JobExperienceRequirement)
            .ThenInclude(j => j.TechnologyRequirements)
            .ThenInclude(t => t.TechnologyType)
            .Include(v => v.LanguageLevelRequirements)
            .ToListAsync();
    }
}