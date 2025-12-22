using Application.Interfaces;

namespace Infrastructure.Data.Seeders;

public class DatabaseSeeder(IApplicationDbContext db, VacancySeeder vacancySeeder, ResumeSeeder resumeSeeder,CompanySeeder companySeeder)
{
    public async Task SeedDbEntitiesAsync()
    {
        await companySeeder.SeedAsync();
        await vacancySeeder.SeedAsync();
        await resumeSeeder.SeedAsync();
        await db.SaveChangesAsync();
    }
}