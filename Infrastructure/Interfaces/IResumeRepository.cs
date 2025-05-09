using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IResumeRepository
{
    Task<ICollection<Resume>> GetAllResumesByVacancyId(int vacancyId);
    Task<Resume> GetResumeById(int resumeId);
}