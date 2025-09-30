using Application.Dtos.Resume;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class ResumeService(IUnitOfWork unitOfWork, ICrmService crmService) : IResumeService
{
    private readonly IRepository<Resume> _repository = unitOfWork.Repository<Resume>();
    private readonly IResumeRepository _resumeRepository = unitOfWork.Resumes;

    public async Task<ResumeResponseDto> CreateResumeAsync(ResumeRequestDto dto)
    {
        var resume = dto.Adapt<Resume>();
        var createdResume = await _repository.CreateAsync(resume);
        createdResume.VacancyResumes = new List<VacancyResume>
        {
            new()
            {
                ResumeId = createdResume.Id,
                VacancyId = dto.VacancyId ?? throw new Exception("Resume is not linked to Vacancy")
            }
        };

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
}