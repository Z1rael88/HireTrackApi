using Domain.Enums;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using LanguageLevel = Domain.Enums.LanguageLevel;

namespace Infrastructure.Data.Seeders;

public class ResumeSeeder(IApplicationDbContext db)
{
    public async Task SeedAsync()
    {
        if (db.Resumes.Any())
            return;

        var dotNetTech = await db.TechnologyTypes.FirstOrDefaultAsync(x => x.Name == ".NET");

        var resume1 = new Resume
        {
            CandidateId = 1,
            Candidate = new Candidate
            {
                Firstname = "Pavel",
                Lastname = "Mazur",
                Bio =
                    "Jestem doświadczonym Senior .NET Developerem z ponad pięcioletnią praktyką w tworzeniu skalowalnych aplikacji webowych i usług backendowych. Specjalizuję się w .NET 7/8, C#, EF Core oraz architekturze opartej na microservices i wzorcach DDD. Pracowałem nad projektami o wysokiej dostępności, optymalizując wydajność, eliminując wąskie gardła i wprowadzając usprawnienia w procesach CI/CD. Cenię czysty kod, automatyzację oraz testy jednostkowe i integracyjne. W pracy stawiam na współpracę, jakość i ciągłe doskonalenie zarówno technologiczne, jak i produktowe.",
                Age = 28,
                Email = "pavelmzr@gmail.com",
                Address = new Address
                {
                    Country = "Poland",
                    City = "Gdansk"
                },
                WorkType = new List<WorkType> { WorkType.Hybrid, WorkType.Remote },
            },
            YearsOfExperience = 5,
            ExpectedSalary = 24000,
            LanguageLevels = new List<Domain.Models.LanguageLevel>
            {
                new() { Language = Language.Polish, Level = LanguageLevel.Native },
                new() { Language = Language.English, Level = LanguageLevel.Advanced }
            },
            JobExperiences = new List<JobExperience>
            {
                new()
                {
                    NameOfCompany = "JustJoinIT",
                    StartDate = DateOnly.Parse("2020-02-01"),
                    EndDate = DateOnly.Parse("2022-02-01"),
                    Technologies = new List<Technology>
                    {
                        new() { TechnologyType = dotNetTech, YearsOfExperience = 5 }
                    },
                    Description =
                        "W JustJoinIT odpowiadałem za rozwój i utrzymanie kluczowych modułów aplikacji w oparciu o platformę .NET. Tworzyłem nowe funkcjonalności, optymalizowałem istniejące rozwiązania oraz dbałem o wysoką jakość kodu poprzez testy jednostkowe i przeglądy kodu..."
                },
                new()
                {
                    NameOfCompany = "Google",
                    StartDate = DateOnly.Parse("2022-02-01"),
                    EndDate = DateOnly.Parse("2025-05-01"),
                    Technologies = new List<Technology>
                    {
                        new() { TechnologyType = dotNetTech, YearsOfExperience = 5 }
                    },
                    Description =
                        "W Google pracowałem nad tworzeniem i rozwijaniem wysoko skalowalnych usług backendowych, wykorzystując platformę .NET oraz nowoczesne rozwiązania chmurowe..."
                }
            },
            Educations = new List<Education>
            {
                new()
                {
                    Title = "Vistula University",
                    EducationType = EducationType.University,
                    Degree = Degree.Bachelors,
                    StartDate = DateOnly.Parse("2015-09-04"),
                    EndDate = DateOnly.Parse("2018-09-04"),
                    Description =
                        "Podczas studiów licencjackich na Vistula University zdobyłem solidne podstawy z zakresu informatyki, programowania oraz inżynierii oprogramowania..."
                }
            },
            VacancyResumes = new List<VacancyResume>
            {
                new()
                {
                    VacancyId = 1,
                    Status = ResumeStatus.Sent
                }
            }
        };

        await db.Resumes.AddAsync(resume1);
        await db.SaveChangesAsync();
    }
}
