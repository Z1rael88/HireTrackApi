using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VacancyRepository(ApplicationDbContext dbContext) : IVacancyRepository
{
    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCompanyIdAsync(int companyId)
    {
        return await dbContext.Vacancies.Where(x => x.CompanyId == companyId)
            .Include(x => x.EducationsRequirement)
            .Include(x => x.JobExperienceRequirement)
            .ThenInclude(x=>x.TechnologyRequirements)
            .ThenInclude(x=>x.TechnologyType)
            .Include(x => x.LanguageLevelRequirements).ToListAsync();
    }

    public async Task<Vacancy> GetByIdAsync(int vacancyId)
    {
        var result = await dbContext.Vacancies
            .Where(x => x.Id == vacancyId)
            .Include(x => x.EducationsRequirement)
            .Include(x => x.JobExperienceRequirement)
            .ThenInclude(x => x.TechnologyRequirements)
            .ThenInclude(x => x.TechnologyType)
            .Include(x => x.LanguageLevelRequirements).FirstOrDefaultAsync();
        if (result is null)
        {
            throw new NotFoundException($"Entity with id {vacancyId} not found");
        }
        return result;
    }
    public async Task<IEnumerable<Vacancy>> GetByHrIdAsync(int hrId)
    {
        var result = await dbContext.Vacancies
            .Where(x => x.HrId == hrId)
            .Include(x => x.EducationsRequirement)
            .Include(x => x.JobExperienceRequirement)
            .ThenInclude(x => x.TechnologyRequirements)
            .ThenInclude(x => x.TechnologyType)
            .Include(x => x.LanguageLevelRequirements).ToListAsync();
        if (result is null)
        {
            throw new NotFoundException($"Entities with hr id {hrId} not found");
        }
        return result;
    }

    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByIdsAsync(List<int> vacancyIds)
    {
        return await dbContext.Vacancies
            .Where(v => vacancyIds.Contains(v.Id))
            .Include(v => v.EducationsRequirement)
            .Include(v => v.JobExperienceRequirement)
            .ThenInclude(j => j.TechnologyRequirements)
            .ThenInclude(t => t.TechnologyType)
            .Include(v => v.LanguageLevelRequirements)
            .ToListAsync();
    }

    public async Task<ResumeStatus> GetResumeStatusByResumeIdAsync(int resumeId)
    {
        return await dbContext.VacancyResumes.Where(x => x.ResumeId == resumeId).Select(x=>x.Status).FirstOrDefaultAsync();
    }

    public async Task UpdateVacancyAsync(Vacancy vacancy, int vacancyId)
    {
        var existingVacancy = await dbContext.Vacancies
            .FirstOrDefaultAsync(v => v.Id == vacancyId);

        if (existingVacancy == null)
            throw new NotFoundException("Vacancy not found");

        UpdateVacancyFields(existingVacancy, vacancy);

        var existingLanguageLevels = await dbContext.LanguageLevelRequirements
            .Where(l => l.VacancyId == vacancyId)
            .ToListAsync();

        var existingEducation = await dbContext.EducationRequirements
            .FirstOrDefaultAsync(e => e.VacancyId == vacancyId);

        var existingJobExperience = await dbContext.JobExperienceRequirements
            .FirstOrDefaultAsync(j => j.VacancyId == vacancyId);

        UpdateLanguageLevels(
            existingLanguageLevels,
            vacancy.LanguageLevelRequirements.ToList(),
            vacancyId);

        UpdateEducation(
            existingEducation,
            vacancy.EducationsRequirement,
            vacancyId);

        await UpdateJobExperience(
            existingJobExperience,
            vacancy.JobExperienceRequirement,
            vacancyId);

        await dbContext.SaveChangesAsync();
    }
    
    private void UpdateVacancyFields(Vacancy existing, Vacancy incoming)
    {
        existing.YearsOfExperience = incoming.YearsOfExperience;
        existing.Description = incoming.Description;
        existing.WorkType = incoming.WorkType;
        existing.Responsibilities = incoming.Responsibilities;
        existing.AddDate = incoming.AddDate;
        existing.EndDate = incoming.EndDate;
        existing.Salary = incoming.Salary;
        existing.Address.City = incoming.Address.City;
        existing.Address.Country = incoming.Address.Country;
    }

    private void UpdateLanguageLevels(
        List<LanguageLevelRequirement> existing,
        List<LanguageLevelRequirement> incoming,
        int vacancyId)
    {
        foreach (var existingItem in existing)
        {
            var incomingItem = incoming.FirstOrDefault(i => i.Id == existingItem.Id);
            if (incomingItem == null) continue;

            existingItem.Language = incomingItem.Language;
            existingItem.Level = incomingItem.Level;
        }

        var toAdd = incoming
            .Where(i => i.Id == 0)
            .Select(i => new LanguageLevelRequirement
            {
                VacancyId = vacancyId,
                Language = i.Language,
                Level = i.Level
            });

        dbContext.LanguageLevelRequirements.AddRange(toAdd);

        var toRemove = existing
            .Where(e => incoming.All(i => i.Id != e.Id))
            .ToList();

        dbContext.LanguageLevelRequirements.RemoveRange(toRemove);
    }

    private void UpdateEducation(
        EducationRequirement? existing,
        EducationRequirement incoming,
        int vacancyId)
    {
        if (existing == null)
        {
            dbContext.EducationRequirements.Add(new EducationRequirement
            {
                VacancyId = vacancyId,
                Degree = incoming.Degree,
                EducationType = incoming.EducationType
            });
            return;
        }

        existing.Degree = incoming.Degree;
        existing.EducationType = incoming.EducationType;
    }

    private async Task UpdateJobExperience(
        JobExperienceRequirement? existing,
        JobExperienceRequirement incoming,
        int vacancyId)
    {
        if (existing == null)
        {
            existing = new JobExperienceRequirement
            {
                VacancyId = vacancyId
            };

            dbContext.JobExperienceRequirements.Add(existing);
        }

        await UpdateTechnologiesAsync(
            existing,
            incoming.TechnologyRequirements.ToList());
    }

    private async Task UpdateTechnologiesAsync(
        JobExperienceRequirement existingJob,
        List<TechnologyRequirement> incomingTechnologies)
    {
        var existingTechnologies = await dbContext.TechnologyRequirements
            .Where(t => t.JobExperienceRequirementId == existingJob.Id)
            .ToListAsync();

        foreach (var existingItem in existingTechnologies)
        {
            var incomingItem = incomingTechnologies.FirstOrDefault(i => i.Id == existingItem.Id && i.JobExperienceRequirementId == existingItem.JobExperienceRequirementId);
            if (incomingItem == null) continue;

            existingItem.TechnologyType = incomingItem.TechnologyType;
            existingItem.TechnologyTypeId = incomingItem.TechnologyTypeId;
            existingItem.YearsOfExperience = incomingItem.YearsOfExperience;
        }

        var toAdd = incomingTechnologies
            .Where(i => i.Id == 0)
            .Select(i => new TechnologyRequirement
            {
                JobExperienceRequirementId = existingJob.Id,
                TechnologyType = i.TechnologyType,
                TechnologyTypeId = i.TechnologyTypeId,
                YearsOfExperience = i.YearsOfExperience
            });

        dbContext.TechnologyRequirements.AddRange(toAdd);

        var toRemove = existingTechnologies
            .Where(e => incomingTechnologies.All(i => i.Id != e.Id))
            .ToList();

        dbContext.TechnologyRequirements.RemoveRange(toRemove);
    }

}