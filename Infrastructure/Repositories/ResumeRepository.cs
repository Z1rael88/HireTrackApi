using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
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
}