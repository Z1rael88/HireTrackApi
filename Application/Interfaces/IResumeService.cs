using Application.Dtos.Resume;

namespace Application.Interfaces;

public interface IResumeService
{
    Task<ResumeResponseDto> CreateResumeAsync(ResumeRequestDto dto);
    Task<ResumeResponseDto> GetResumeByIdAsync(int resumeId);
    Task<IEnumerable<ResumeResponseDto>> GetAllResumesByVacancyIdAsync(int vacancyId);

}