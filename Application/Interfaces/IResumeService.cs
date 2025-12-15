using Application.Dtos.Resume;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IResumeService
{
    Task<ResumeResponseDto?> CreateResumeAsync(ResumeRequestDto dto);
    Task<ResumeResponseDto> GetResumeByIdAsync(int resumeId);
    Task<IEnumerable<ResumeResponseDto>> GetAllResumesByVacancyIdAsync(int vacancyId);
    Task ChangeStatusOfResumeAsync(int resumeId,int vacancyId, ResumeStatus status);
    ResumeResponseDto UploadResumeAsync(IFormFile resume);
    Task UpdateResumeAsync(ResumeRequestDto dto, int resumeId);
    Task<ResumeResponseDto> GetResumeByUserIdAsync(int userId);

}