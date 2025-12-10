using Application.Dtos.Resume;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class ResumeService(IUnitOfWork unitOfWork,IEmailService emailService,UserManager<User> userManager) : IResumeService
{
    private readonly IRepository<Resume> _repository = unitOfWork.Repository<Resume>();
    private readonly IRepository<VacancyResume> _vacancyResumeRepository = unitOfWork.Repository<VacancyResume>();
    private readonly IResumeRepository _resumeRepository = unitOfWork.Resumes;
    private readonly ICandidateRepository _candidateCommonRepository = unitOfWork.Candidates;
    private readonly IRepository<Candidate> _candidateRepository = unitOfWork.Repository<Candidate>();
    private readonly IVacancyRepository _vacancyRepository = unitOfWork.Vacancies;


    public async Task<ResumeResponseDto?> CreateResumeAsync(ResumeRequestDto dto)
    {
        var resume = dto.Adapt<Resume>();
        var existingResume = await _resumeRepository.GetResumeByCandidateEmail(resume.Candidate.Email);
        if (dto.VacancyId is not 0 && existingResume is not null)
        {
            var vacancyResumes = new VacancyResume
            {
                    ResumeId = existingResume.Id,
                    VacancyId = (int)dto.VacancyId!,
                    Status = ResumeStatus.Sent,
            };
            await _vacancyResumeRepository.CreateAsync(vacancyResumes);
            return null;
        }
        var createdResume = await _repository.CreateAsync(resume);
        if (dto.VacancyId is not 0)
        {
            createdResume.VacancyResumes = new List<VacancyResume>
            {
                new()
                {
                    VacancyId = (int)dto.VacancyId!,
                    ResumeId = createdResume.Id,
                    Status = ResumeStatus.Sent
                }
            };
        }
        
        var user = await userManager.FindByEmailAsync(resume.Candidate.Email);
        var candidate = await _candidateCommonRepository.GetCandidateByEmailAsync(resume.Candidate.Email);
        if ( user is not null && candidate is not null)
        {
            candidate.UserId = user.Id;
            await _candidateRepository.UpdateAsync(resume.Candidate);
        }
        
        var result = createdResume.Adapt<ResumeResponseDto>();
        return result;
    }

    public async Task<ResumeResponseDto> GetResumeByIdAsync(int resumeId)
    {
        var resume = await _resumeRepository.GetResumeById(resumeId);
        var result = resume.Adapt<ResumeResponseDto>();
        result.Status = await _vacancyRepository.GetResumeStatusByResumeIdAsync(resumeId);
        return result;
    }

    public async Task<IEnumerable<ResumeResponseDto>> GetAllResumesByVacancyIdAsync(int vacancyId)
    {
        var resumes = await _resumeRepository.GetAllResumesByVacancyId(vacancyId);
        return resumes.Adapt<ICollection<ResumeResponseDto>>();
    }

    public async Task ChangeStatusOfResumeAsync(int resumeId, int vacancyId, ResumeStatus status)
    {
        var vacancyResumes = await _resumeRepository.GetVacancyResumeByIds(vacancyId, resumeId);
        vacancyResumes.Status = status;
        await _vacancyResumeRepository.SaveChangesAsync();
        var resume = await GetResumeByIdAsync(resumeId);
        await emailService.SendEmailAsync(resume.Candidate.Email, "Status for your resume changed!",
            $"Hello {resume.Candidate.Firstname} !" +
            $"Status for your resume changed to {status}");
    }

    public async Task UpdateResumeAsync(ResumeRequestDto dto, int resumeId)
    {
        await _resumeRepository.UpdateResume(dto.Adapt<Resume>(),resumeId);
    }

    public async Task<ResumeResponseDto> GetResumeByUserIdAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user!.Email is null) throw new NotFoundException("No user found with that Id");
        var resume = await _resumeRepository.GetResumeByCandidateEmail(user.Email);
        var result = resume.Adapt<ResumeResponseDto>();
        result.Status = await _vacancyRepository.GetResumeStatusByResumeIdAsync(resume.Id);
        return result;
    }
}