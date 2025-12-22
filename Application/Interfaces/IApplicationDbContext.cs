using Domain.Models;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    IQueryable<Vacancy> Vacancies { get; }
    IQueryable<Resume> Resumes { get; }
    IQueryable<Education> Educations { get; }
    IQueryable<Technology> Technologies { get; }
    IQueryable<TechnologyType> TechnologyTypes { get; }
    IQueryable<JobExperience> JobExperiences { get; }
    IQueryable<Company> Companies { get; }
    IQueryable<LanguageLevel> LanguageLevels { get; }
    IQueryable<Candidate> Candidates { get; }
    IQueryable<Statistics> Statistics { get; }
    IQueryable<LanguageLevelRequirement> LanguageLevelRequirements { get; }

    IQueryable<EducationRequirement> EducationRequirements { get; }
    IQueryable<JobExperienceRequirement> JobExperienceRequirements { get; }

    IQueryable<VacancyResume> VacancyResumes { get; }
    Task<int> SaveChangesAsync();
}