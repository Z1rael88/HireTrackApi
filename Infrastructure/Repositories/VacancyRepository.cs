using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VacancyRepository(IApplicationDbContext dbContext) : IVacancyRepository
{
    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCompanyId(int companyId)
    {
        return await dbContext.Vacancies.Where(x => x.CompanyId == companyId).ToListAsync();
    }
}