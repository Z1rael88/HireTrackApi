using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResumeRepository(ApplicationDbContext dbContext) : Repository<Resume>(dbContext), IResumeRepository
{
    public async Task<ICollection<Resume>> GetAllResumesByVacancyId(int vacancyId)
    {
        return await dbContext.Resumes
            .AsNoTracking()
            .Include(x => x.Candidate)
            .Include(x => x.Educations)
            .Include(x => x.LanguageLevels)
            .Include(x => x.JobExperiences)
            .ThenInclude(x => x.Technologies)
            .ThenInclude(x => x.TechnologyType)
            .Where(r => dbContext.VacancyResumes
                .Any(vr => vr.VacancyId == vacancyId && vr.ResumeId == r.Id)).ToListAsync();
    }

    public async Task<Resume> GetResumeById(int resumeId)
    {
        var query = dbContext.Resumes.AsNoTracking()
            .Where(x => x.Id == resumeId)
            .Include(x => x.Candidate)
            .Include(x => x.Educations)
            .Include(x => x.LanguageLevels)
            .Include(x => x.JobExperiences)
            .ThenInclude(x => x.Technologies)
            .ThenInclude(x => x.TechnologyType);
        return await query.SingleOrDefaultAsync() ??
               throw new NotFoundException($"Resume with Id: {resumeId} not found");
    }

    public async Task<VacancyResume> GetVacancyResumeByIds(int vacancyId, int resumeId)
    {
        var query = dbContext.VacancyResumes
            .Where(x => x.VacancyId == vacancyId && x.ResumeId == resumeId);
        return await query.SingleOrDefaultAsync() ??
               throw new NotFoundException("VacancyResume entity with those ids were not found");
    }

    public async Task<Resume?> GetResumeByCandidateEmail(string email)
    {
        var resume = await dbContext.Resumes
            .Include(x => x.Candidate)
            .Include(x => x.Educations)
            .Include(x => x.LanguageLevels)
            .Include(x => x.JobExperiences)
            .ThenInclude(x => x.Technologies)
            .ThenInclude(x => x.TechnologyType)
            .FirstOrDefaultAsync(x => x.Candidate.Email == email);
        return resume;
    }

    public async Task<IEnumerable<VacancyResume>> GetAllVacancyResumesByResumeIdAsync(int resumeId)
    {
        return await dbContext.VacancyResumes.Where(x => x.ResumeId == resumeId).Include(x => x.Vacancy).ToListAsync();
    }

    public async Task UpdateResume(Resume resume, int resumeId)
    {
        var existingResume = await dbContext.Resumes
            .Include(r => r.Candidate)
            .FirstOrDefaultAsync(r => r.Id == resumeId);

        if (existingResume == null)
            throw new NotFoundException("Resume not found");

        UpdateResumeFields(existingResume, resume);
        UpdateCandidate(existingResume.Candidate, resume.Candidate);

        var existingLanguageLevels = await dbContext.LanguageLevels
            .Where(l => l.ResumeId == resumeId)
            .ToListAsync();

        var existingEducations = await dbContext.Educations
            .Where(e => e.ResumeId == resumeId)
            .ToListAsync();

        var existingJobExperiences = await dbContext.JobExperiences
            .Where(j => j.ResumeId == resumeId)
            .ToListAsync();

        /*var existingTechnologies = await dbContext.Technologies
            .Where(t => existingJobExperiences.Select(j => j.Id).Contains(t.JobExperienceId))
            .ToListAsync();*/

        UpdateLanguageLevels(existingLanguageLevels, resume.LanguageLevels.ToList(), resumeId);
        UpdateEducations(existingEducations, resume.Educations.ToList(), resumeId);
        await UpdateJobExperiencesAsync(existingJobExperiences, resume.JobExperiences.ToList(), resumeId);

        await dbContext.SaveChangesAsync();
    }


    private void UpdateResumeFields(Resume existing, Resume incoming)
    {
        existing.YearsOfExperience = incoming.YearsOfExperience;
        existing.ExpectedSalary = incoming.ExpectedSalary;
    }

    private void UpdateCandidate(Candidate existing, Candidate incoming)
    {
        existing.Firstname = incoming.Firstname;
        existing.Lastname = incoming.Lastname;
        existing.Email = incoming.Email;
        existing.Bio = incoming.Bio;
        existing.Address = incoming.Address;
        existing.WorkType = incoming.WorkType;
        existing.Age = incoming.Age;
    }

    private void UpdateLanguageLevels(List<LanguageLevel> existing, List<LanguageLevel> incoming, int resumeId)
    {
        var toUpdate = existing.Where(e => incoming.Any(i => i.Id == e.Id)).ToList();
        foreach (var existingItem in toUpdate)
        {
            var incomingItem = incoming.First(i => i.Id == existingItem.Id);
            existingItem.Language = incomingItem.Language;
            existingItem.Level = incomingItem.Level;
        }

        dbContext.LanguageLevels.UpdateRange(toUpdate);

        var toAdd = incoming.Where(i => i.Id == 0)
            .Select(i => new LanguageLevel
            {
                ResumeId = resumeId,
                Language = i.Language,
                Level = i.Level
            })
            .ToList();
        dbContext.LanguageLevels.AddRange(toAdd);

        var toRemove = existing.Where(e => incoming.All(i => i.Id != e.Id)).ToList();
        dbContext.LanguageLevels.RemoveRange(toRemove);
    }

    private void UpdateEducations(List<Education> existing, List<Education> incoming, int resumeId)
    {
        var toUpdate = existing.Where(e => incoming.Any(i => i.Id == e.Id)).ToList();
        foreach (var existingItem in toUpdate)
        {
            var incomingItem = incoming.First(i => i.Id == existingItem.Id);
            existingItem.Title = incomingItem.Title;
            existingItem.Degree = incomingItem.Degree;
            existingItem.Description = incomingItem.Description;
            existingItem.StartDate = incomingItem.StartDate;
            existingItem.EndDate = incomingItem.EndDate;
            existingItem.EducationType = incomingItem.EducationType;
        }

        dbContext.Educations.UpdateRange(toUpdate);

        var toAdd = incoming.Where(i => i.Id == 0)
            .Select(i => new Education
            {
                ResumeId = resumeId,
                Title = i.Title,
                Degree = i.Degree,
                Description = i.Description,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                EducationType = i.EducationType
            })
            .ToList();
        dbContext.Educations.AddRange(toAdd);

        var toRemove = existing.Where(e => incoming.All(i => i.Id != e.Id)).ToList();
        dbContext.Educations.RemoveRange(toRemove);
    }

    private async Task UpdateJobExperiencesAsync(List<JobExperience> existing, List<JobExperience> incoming,
        int resumeId)
    {
        var toUpdate = existing.Where(e => incoming.Any(i => i.Id == e.Id)).ToList();
        foreach (var existingItem in toUpdate)
        {
            var incomingItem = incoming.First(i => i.Id == existingItem.Id);
            existingItem.NameOfCompany = incomingItem.NameOfCompany;
            existingItem.StartDate = incomingItem.StartDate;
            existingItem.EndDate = incomingItem.EndDate;
            existingItem.Description = incomingItem.Description;

            await UpdateTechnologiesAsync(existingItem, incomingItem.Technologies.ToList());
        }

        dbContext.JobExperiences.UpdateRange(toUpdate);

        var toAdd = incoming.Where(i => i.Id == 0)
            .Select(i => new JobExperience
            {
                ResumeId = resumeId,
                NameOfCompany = i.NameOfCompany,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                Description = i.Description,
                Technologies = i.Technologies.Select(t => new Technology
                {
                    TechnologyType = t.TechnologyType,
                    TechnologyTypeId = t.TechnologyTypeId,
                    YearsOfExperience = t.YearsOfExperience
                }).ToList()
            })
            .ToList();
        dbContext.JobExperiences.AddRange(toAdd);

        var toRemove = existing.Where(e => incoming.All(i => i.Id != e.Id)).ToList();
        dbContext.JobExperiences.RemoveRange(toRemove);
    }

    private async Task UpdateTechnologiesAsync(JobExperience existingJob, List<Technology> incomingTechnologies)
    {
        var existingTechnologies = await dbContext.Technologies
            .Where(t => t.JobExperienceId == existingJob.Id)
            .ToListAsync();

        var toUpdate = existingTechnologies.Where(e => incomingTechnologies.Any(i => i.Id == e.Id)).ToList();
        foreach (var existingItem in toUpdate)
        {
            var incomingItem = incomingTechnologies.First(i => i.Id == existingItem.Id);
            existingItem.TechnologyType = incomingItem.TechnologyType;
            existingItem.TechnologyTypeId = incomingItem.TechnologyTypeId;
            existingItem.YearsOfExperience = incomingItem.YearsOfExperience;
        }

        dbContext.Technologies.UpdateRange(toUpdate);

        var toAdd = incomingTechnologies.Where(i => i.Id == 0)
            .Select(i => new Technology
            {
                JobExperienceId = existingJob.Id,
                TechnologyType = i.TechnologyType,
                TechnologyTypeId = i.TechnologyTypeId,
                YearsOfExperience = i.YearsOfExperience
            })
            .ToList();
        dbContext.Technologies.AddRange(toAdd);

        var toRemove = existingTechnologies.Where(e => incomingTechnologies.All(i => i.Id != e.Id)).ToList();
        dbContext.Technologies.RemoveRange(toRemove);
    }
}