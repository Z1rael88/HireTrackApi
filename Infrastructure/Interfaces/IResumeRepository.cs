using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IResumeRepository
{
    Task<ICollection<Resume>> GetAllResumesByVacancyId(int vacancyId);
    Task<Resume> GetResumeById(int resumeId);
    Task<VacancyResume> GetVacancyResumeByIds(int vacancyId, int resumeId);
    Task<Resume?> GetResumeByCandidateEmail(string email);
    Task<IEnumerable<VacancyResume>?> GetAllVacancyResumesByResumeIdAsync(int resumeId);
    Task UpdateResume(Resume resume, int resumeId);
}