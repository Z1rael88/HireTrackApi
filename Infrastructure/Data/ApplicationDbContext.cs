using Domain.Models;
using Infrastructure.Data.Configurations;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, IdentityRole<int>, int>(options), IApplicationDbContext
{
    public DbSet<User> ApplicationUsers { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Resume> Resumes { get; }
    public DbSet<Language> Languages { get; }
    public DbSet<ResumeLanguage> ResumeLanguages { get; }
    public DbSet<Education> Educations { get; }
    public DbSet<Technology> Technologies { get; }
    public DbSet<TechnologyType> TechnologyTypes { get; }
    public DbSet<JobExperience> JobExperiences { get; }
    public DbSet<Company> Companies { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VacancyConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        modelBuilder.ApplyConfiguration(new ResumeConfiguration());
        modelBuilder.ApplyConfiguration(new ResumeLanguageConfiguration());
        modelBuilder.ApplyConfiguration(new TechnologyConfiguration());
        modelBuilder.ApplyConfiguration(new TechnologyTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EducationConfiguration());
        modelBuilder.ApplyConfiguration(new JobExperienceConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
    }
}