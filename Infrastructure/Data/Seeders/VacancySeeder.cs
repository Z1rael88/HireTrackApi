using Domain.Enums;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LanguageLevel = Domain.Enums.LanguageLevel;

namespace Infrastructure.Data.Seeders;

public class VacancySeeder(IApplicationDbContext db, UserManager<User> userManager)
{
    public async Task SeedAsync()
    {
        if (db.Vacancies.Any())
            return;

        var hrManager = await userManager.FindByEmailAsync("hrManager1@gmail.com");
        if (hrManager == null)
        {
            hrManager = new User
            {
                Id = 2,
                UserName = "hrManager1@gmail.com",
                Email = "hrManager1@gmail.com",
                Firstname = "Oliwia",
                Lastname = "Kowalska",
                Age = 26,
                CompanyId = 1
            };
            await userManager.CreateAsync(hrManager, "P@ssword1");
            await userManager.AddToRoleAsync(hrManager, Role.HrManager.ToString());
        }

        var dotNetTech = await db.TechnologyTypes.FirstOrDefaultAsync(x => x.Name == ".NET") 
                         ?? new TechnologyType { Name = ".NET", LogoUrl = "logo", TechnologyCategory = TechnologyCategory.Backend };

        var typescriptTech = await db.TechnologyTypes.FirstOrDefaultAsync(x => x.Name == "Typescript") 
                             ?? new TechnologyType { Name = "Typescript", LogoUrl = "logo", TechnologyCategory = TechnologyCategory.Frontend };

        var reactTech = await db.TechnologyTypes.FirstOrDefaultAsync(x => x.Name == "React") 
                        ?? new TechnologyType { Name = "React", LogoUrl = "logo", TechnologyCategory = TechnologyCategory.Frontend };

        var vacancy1 = new Vacancy
        {
            Name = "Senior .NET Developer z 5+ latami doświadczenia",
            Description =
                "Poszukujemy doświadczonego Senior .NET Developera z minimum 5-letnim doświadczeniem komercyjnym...",
            Salary = 24000,
            AddDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.Parse("2026-02-02"),
            HrId = hrManager.Id,
            CompanyId = 1,
            YearsOfExperience = 5,
            Address = new Address { Country = "Poland", City = "Warsaw" },
            WorkType = WorkType.OnSite,
            Responsibilities = "Projektowanie i rozwój usług backendowych w oparciu o .NET 6/7/8; ...",
            LanguageLevelRequirements = new List<LanguageLevelRequirement>
            {
                new() { Language = Language.English, Level = LanguageLevel.UpperIntermediate },
                new() { Language = Language.Polish, Level = LanguageLevel.Native }
            },
            JobExperienceRequirement = new JobExperienceRequirement
            {
                TechnologyRequirements = new List<TechnologyRequirement>
                {
                    new() { TechnologyType = dotNetTech, YearsOfExperience = 2 }
                }
            },
            EducationsRequirement = new EducationRequirement
            {
                EducationType = EducationType.University,
                Degree = Degree.Bachelors
            },
        };

        var vacancy2 = new Vacancy
        {
            Name = "Senior React Developer z 5+ latami doświadczenia",
            Description =
                "Poszukujemy doświadczonego Senior React Developera z minimum 5-letnim doświadczeniem...",
            Salary = 24000,
            AddDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.Parse("2026-02-02"),
            HrId = hrManager.Id,
            CompanyId = 1,
            YearsOfExperience = 5,
            Address = new Address { Country = "Poland", City = "Warsaw" },
            WorkType = WorkType.Hybrid,
            Responsibilities =
                "Projektowanie i rozwój zaawansowanych aplikacji frontendowych w oparciu o React i TypeScript; ...",
            LanguageLevelRequirements = new List<LanguageLevelRequirement>
            {
                new() { Language = Language.English, Level = LanguageLevel.UpperIntermediate },
                new() { Language = Language.Polish, Level = LanguageLevel.UpperIntermediate }
            },
            JobExperienceRequirement = new JobExperienceRequirement
            {
                TechnologyRequirements = new List<TechnologyRequirement>
                {
                    new() { TechnologyType = typescriptTech, YearsOfExperience = 5 },
                    new() { TechnologyType = reactTech, YearsOfExperience = 2 }
                }
            },
            EducationsRequirement = new EducationRequirement
            {
                EducationType = EducationType.University,
                Degree = Degree.Masters
            },
        };

        await db.Vacancies.AddRangeAsync(vacancy1, vacancy2);
        await db.SaveChangesAsync();
    }
}
