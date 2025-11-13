using Application.Dtos.Resume;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class ResumeService(IUnitOfWork unitOfWork, ICrmService crmService,IEmailService emailService) : IResumeService
{
    private readonly IRepository<Resume> _repository = unitOfWork.Repository<Resume>();
    private readonly IRepository<VacancyResume> _vacancyResumeRepository = unitOfWork.Repository<VacancyResume>();
    private readonly IResumeRepository _resumeRepository = unitOfWork.Resumes;

    public async Task<ResumeResponseDto> CreateResumeAsync(ResumeRequestDto dto)
    {
        var resume = dto.Adapt<Resume>();
        var createdResume = await _repository.CreateAsync(resume);
        if (dto.VacancyId is not 0)
        {
            createdResume.VacancyResumes = new List<VacancyResume>
            {
                new()
                {
                    ResumeId = createdResume.Id,
                    VacancyId = (int)dto.VacancyId!,
                    Status = ResumeStatus.Sent,
                }
            };
        }

        await _repository.SaveChangesAsync();

        var typeIds = createdResume.JobExperiences
            .SelectMany(x => x.Technologies)
            .Select(t => t.TechnologyTypeId)
            .Distinct();

        var types = await crmService.GetAllTechnologyTypes();
        var mappedTypes = types.Adapt<IList<TechnologyType>>();
        var matchingTypes = mappedTypes
            .Where(tt => typeIds.Contains(tt.Id)).ToDictionary(tt => tt.Id);

        foreach (var tech in createdResume.JobExperiences.SelectMany(x => x.Technologies))
        {
            tech.TechnologyType = matchingTypes[tech.TechnologyTypeId];
        }

        var result = createdResume.Adapt<ResumeResponseDto>();
        return result;
    }

    public async Task<ResumeResponseDto> GetResumeByIdAsync(int resumeId)
    {
        var resume = await _resumeRepository.GetResumeById(resumeId);
        return resume.Adapt<ResumeResponseDto>();
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
}