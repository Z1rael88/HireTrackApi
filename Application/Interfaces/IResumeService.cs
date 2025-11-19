using Application.Dtos.Resume;
using Domain.Enums;

namespace Application.Interfaces;

public interface IResumeService
{
    Task<ResumeResponseDto?> CreateResumeAsync(ResumeRequestDto dto);
    Task<ResumeResponseDto> GetResumeByIdAsync(int resumeId);
    Task<IEnumerable<ResumeResponseDto>> GetAllResumesByVacancyIdAsync(int vacancyId);
    Task ChangeStatusOfResumeAsync(int resumeId,int vacancyId, ResumeStatus status);
    Task UpdateResumeAsync(ResumeRequestDto dto, int resumeId);

}