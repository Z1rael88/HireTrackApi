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
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<TechnologyType> TechnologyTypes { get; set; }
    public DbSet<JobExperience> JobExperiences { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<LanguageLevel> LanguageLevels { get; set; }
    public DbSet<Candidate> Candidates{ get; set; }
    public DbSet<Statistics> Statistics { get; set; }
    public DbSet<LanguageLevelRequirement> LanguageLevelRequirements { get; set; }
    public DbSet<EducationRequirement> EducationRequirements { get; set; }
    public DbSet<JobExperienceRequirement> JobExperienceRequirements { get; set; }
    public DbSet<VacancyResume> VacancyResumes { get; set; }
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
        modelBuilder.ApplyConfiguration(new LanguageLevelConfiguration());
        modelBuilder.ApplyConfiguration(new ResumeConfiguration());
        modelBuilder.ApplyConfiguration(new TechnologyConfiguration());
        modelBuilder.ApplyConfiguration(new TechnologyTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EducationConfiguration());
        modelBuilder.ApplyConfiguration(new JobExperienceConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new CandidateConfiguration());
        modelBuilder.ApplyConfiguration(new VacancyResumeConfiguration());
        modelBuilder.ApplyConfiguration(new TechnologyRequirementConfiguration());
        modelBuilder.ApplyConfiguration(new LanguageLevelRequirementConfiguration());
        modelBuilder.ApplyConfiguration(new JobExperienceRequirementConfiguration());
        modelBuilder.ApplyConfiguration(new EducationRequirementConfiguration());
        modelBuilder.ApplyConfiguration(new StatisticsConfiguration());
    }
}