using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResumeRepository(IApplicationDbContext dbContext) : IResumeRepository
{
    public async Task<ICollection<Resume>> GetAllResumesByVacancyId(int vacancyId)
    {
        return await dbContext.VacancyResumes.AsNoTracking()
            .Where(x => x.VacancyId == vacancyId)
            .Include(x => x.Resume)
            .Select(x => x.Resume)
            .ToListAsync();
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
}