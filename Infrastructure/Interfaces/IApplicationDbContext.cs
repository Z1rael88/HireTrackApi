using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Vacancy> Vacancies { get; }
    DbSet<Resume> Resumes { get; }
    DbSet<Education> Educations { get; }
    DbSet<Technology> Technologies { get; }
    DbSet<TechnologyType> TechnologyTypes { get; }
    DbSet<JobExperience> JobExperiences { get; }
    DbSet<Company> Companies { get; }
    DbSet<LanguageLevel> LanguageLevels { get; }
    DbSet<Candidate> Candidates { get; }
    DbSet<Statistics> Statistics { get; }
    DbSet<LanguageLevelRequirement> LanguageLevelRequirements { get; }

    DbSet<EducationRequirement> EducationRequirements { get; }
    DbSet<JobExperienceRequirement> JobExperienceRequirements { get; }

    DbSet<VacancyResume> VacancyResumes { get; }
    Task<int> SaveChangesAsync();
}