using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VacancyRepository(IApplicationDbContext dbContext) : IVacancyRepository
{
    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCompanyId(int companyId)
    {
        return await dbContext.Vacancies.Where(x => x.CompanyId == companyId)
            .Include(x => x.EducationsRequirements)
            .Include(x => x.JobExperienceRequirements)
            .ThenInclude(x=>x.TechnologyRequirements)
            .ThenInclude(x=>x.TechnologyType)
            .Include(x => x.LanguageLevelRequirements).ToListAsync();
    }
}