using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Vacancy> Vacancies { get; }
    DbSet<Resume> Resumes { get; }
    DbSet<Language> Languages { get; }
    DbSet<ResumeLanguage> ResumeLanguages { get; }
    DbSet<Education> Educations { get; }
    DbSet<Technology> Technologies { get; }
    DbSet<TechnologyType> TechnologyTypes { get; }
    DbSet<JobExperience> JobExperiences { get; }
    DbSet<Company> Companies { get; }


    Task<int> SaveChangesAsync();
}