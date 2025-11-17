using Application.Dtos.Statistics;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;

namespace Application.Services;

public class StatisticService(IUnitOfWork unitOfWork) : IStatisticService
{
    private IRepository<Vacancy> _vacancyRepository = unitOfWork.Repository<Vacancy>();
    private IRepository<Resume> _resumeRepository = unitOfWork.Repository<Resume>();
    
    public async Task<StatisticResponseDto> GenerateStatisticsAsync(int vacancyId, int resumeId)
    {
        var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);
        var resume = await _resumeRepository.GetByIdAsync(resumeId);
        if (resume is null && vacancy  is null)
        {
            throw new NotFoundException("Vacancy and Resume not found");
        }
        
        var statisticsResult = new Statistic();
        statisticsResult.VacancyId = vacancyId;
        statisticsResult.ResumeId = resumeId;

        var education = resume?.Educations.FirstOrDefault(); //fix for multiple educations
       // var educationStatistics = CalculateEducationStatistics(vacancy.EducationsRequirement, education);
       var x = new EducationRequirement
       {
           EducationType = EducationType.University,
           Degree = Degree.Phd
       };
       var y = new Education
       {
           EducationType = EducationType.OnlineCourse,
           Degree = Degree.Bachelors,
       };
       var educationStatistics = CalculateEducationStatistics(x, y);
        statisticsResult.EducationMatchPercent = educationStatistics.ExperienceMatchPercent;
        statisticsResult.EducationSummary = educationStatistics.ExperienceSummary;
        
        return statisticsResult.Adapt<StatisticResponseDto>();
    }

    private ExperienceStatistics CalculateEducationStatistics(
        EducationRequirement educationRequirement,
        Education education)
    {
        var result = new ExperienceStatistics();

        int requiredTypeLevel = (int)educationRequirement.EducationType;
        int requiredDegreeLevel = (int)educationRequirement.Degree;
        int candidateTypeLevel = (int)education.EducationType;
        int candidateDegreeLevel = (int)education.Degree;

        double typeRatio = requiredTypeLevel == 0 ? 1 : (double)candidateTypeLevel / requiredTypeLevel;
        double degreeRatio = requiredDegreeLevel == 0 ? 1 : (double)candidateDegreeLevel / requiredDegreeLevel;

        double matchPercent = (typeRatio * 0.6 + degreeRatio * 0.4);

        matchPercent = Math.Clamp(matchPercent, 0.0, 1.5);

        string summary = matchPercent switch
        {
            > 1.0 => $"Candidate exceeds education requirement: {education.EducationType} ({education.Degree}) " +
                     $"vs required {educationRequirement.EducationType} ({educationRequirement.Degree}).",
            1.0 => $"Candidate perfectly matches education requirement ({education.EducationType}, {education.Degree}).",
            _ => $"Candidate education is below required: {education.EducationType} ({education.Degree}) " +
                 $"vs required {educationRequirement.EducationType} ({educationRequirement.Degree})."
        };

        result.ExperienceMatchPercent = matchPercent;
        result.ExperienceSummary = summary;

        return result;
    }


}