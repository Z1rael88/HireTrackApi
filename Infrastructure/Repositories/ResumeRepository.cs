using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResumeRepository(IApplicationDbContext dbContext) : IResumeRepository
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
        return await dbContext.VacancyResumes.Where(x => x.ResumeId == resumeId).ToListAsync();
    }

    public async Task UpdateResume(Resume resume, int resumeId)
    {
        var existing = await dbContext.Resumes
            .Include(r => r.LanguageLevels)
            .Include(r => r.Educations)
            .Include(r => r.JobExperiences)
            .ThenInclude(j => j.Technologies)
            .FirstOrDefaultAsync(r => r.Id == resumeId);

        if (existing == null)
            throw new NotFoundException("No such resume found");

        resume.Adapt(existing);
        
        foreach (var old in existing.LanguageLevels.ToList())
        {
            if (!resume.LanguageLevels.Any(l => l.Id == old.Id))
                dbContext.LanguageLevels.Remove(old);
        }

        foreach (var incoming in resume.LanguageLevels)
        {
            var existingLevel = existing.LanguageLevels.FirstOrDefault(l => l.Id == incoming.Id);
            if (existingLevel == null)
            {
                existing.LanguageLevels.Add(new LanguageLevel
                {
                    Level = incoming.Level,
                    Language = incoming.Language
                });
            }
            else
            {
                existingLevel.Level = incoming.Level;
                existingLevel.Language = incoming.Language;
            }
        }

        foreach (var old in existing.Educations.ToList())
        {
            if (!resume.Educations.Any(e => e.Id == old.Id))
                dbContext.Educations.Remove(old);
        }

        foreach (var incoming in resume.Educations)
        {
            var existingEdu = existing.Educations.FirstOrDefault(e => e.Id == incoming.Id);
            if (existingEdu == null)
            {
                existing.Educations.Add(new Education
                {
                    Title = incoming.Title,
                    Degree = incoming.Degree,
                    Description = incoming.Description,
                    StartDate = incoming.StartDate,
                    EndDate = incoming.EndDate,
                    EducationType = incoming.EducationType
                });
            }
            else
            {
                existingEdu.Title = incoming.Title;
                existingEdu.Degree = incoming.Degree;
                existingEdu.Description = incoming.Description;
                existingEdu.StartDate = incoming.StartDate;
                existingEdu.EndDate = incoming.EndDate;
                existingEdu.EducationType = incoming.EducationType;
            }
        }

        foreach (var old in existing.JobExperiences.ToList())
        {
            if (!resume.JobExperiences.Any(j => j.Id == old.Id))
                dbContext.JobExperiences.Remove(old);
        }

        foreach (var incoming in resume.JobExperiences)
        {
            var existingJob = existing.JobExperiences.FirstOrDefault(j => j.Id == incoming.Id);
            if (existingJob == null)
            {
                var newJob = new JobExperience
                {
                    NameOfCompany = incoming.NameOfCompany,
                    StartDate = incoming.StartDate,
                    EndDate = incoming.EndDate,
                    Description = incoming.Description,
                    Technologies = incoming.Technologies
                        .Select(t => new Technology
                        {
                            TechnologyType = t.TechnologyType,
                            TechnologyTypeId = t.TechnologyTypeId,
                            YearsOfExperience = t.YearsOfExperience
                        }).ToList()
                };
                existing.JobExperiences.Add(newJob);
            }
            else
            {
                existingJob.NameOfCompany = incoming.NameOfCompany;
                existingJob.StartDate = incoming.StartDate;
                existingJob.EndDate = incoming.EndDate;
                existingJob.Description = incoming.Description;

                foreach (var oldTech in existingJob.Technologies.ToList())
                {
                    if (!incoming.Technologies.Any(t => t.Id == oldTech.Id))
                        dbContext.Technologies.Remove(oldTech);
                }

                foreach (var tech in incoming.Technologies)
                {
                    var existingTech = existingJob.Technologies.FirstOrDefault(t => t.Id == tech.Id);
                    if (existingTech == null)
                    {
                        existingJob.Technologies.Add(new Technology
                        {
                            TechnologyType = tech.TechnologyType,
                            TechnologyTypeId = tech.TechnologyTypeId,
                            YearsOfExperience = tech.YearsOfExperience
                        });
                    }
                    else
                    {
                        existingTech.TechnologyType = tech.TechnologyType;
                        existingTech.TechnologyTypeId = tech.TechnologyTypeId;
                        existingTech.YearsOfExperience = tech.YearsOfExperience;
                    }
                }
            }
        }

        await dbContext.SaveChangesAsync();
    }
}