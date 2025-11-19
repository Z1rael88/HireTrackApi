using System.Xml.Linq;
using Application.Dtos.Statistics;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;
using LanguageLevel = Domain.Models.LanguageLevel;

namespace Application.Services;

public class StatisticService(IUnitOfWork unitOfWork) : IStatisticService
{
    private IVacancyRepository _vacancyRepository = unitOfWork.Vacancies;
    private IResumeRepository _resumeRepository = unitOfWork.Resumes;

    public async Task<StatisticResponseDto> GenerateStatisticsAsync(int vacancyId, int resumeId)
    {
        var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);
        var resume = await _resumeRepository.GetResumeById(resumeId);
        if (resume is null && vacancy is null)
        {
            throw new NotFoundException("Vacancy and Resume not found");
        }

        var statisticsResult = new Statistic();
        statisticsResult.VacancyId = vacancyId;
        statisticsResult.ResumeId = resumeId;

        var education = resume?.Educations.FirstOrDefault(); //fix for multiple educations
        var educationStatistics = CalculateEducationStatistics(vacancy.EducationsRequirement, education);
        statisticsResult.EducationMatchPercent = educationStatistics.ExperienceMatchPercent;
        statisticsResult.EducationSummary = educationStatistics.ExperienceSummary;

        var languageLevel = CalculateLanguageLevelStatistics(vacancy.LanguageLevelRequirements,resume.LanguageLevels);

        return statisticsResult.Adapt<StatisticResponseDto>();
    }

    private LanguageLevelStatistics CalculateLanguageLevelStatistics(ICollection<LanguageLevelRequirement> languageLevelRequirements, ICollection<LanguageLevel> resumeLanguageLevels)
    {
        var result = new LanguageLevelStatistics();

       var requiredLanguages = languageLevelRequirements.ToList();
       var languages = resumeLanguageLevels.ToList();
       var totalRequired = requiredLanguages.Count;
       var matched = requiredLanguages.Count(req =>
           languages.Any(c => c.Language == req.Language && c.Level == req.Level));
       result.MatchPercent = (double)matched / totalRequired;
       switch (result.MatchPercent)
       {
           case 0.0:
               result.Summary = "Candidate does not match required Languages";
               break;
           case < 0.5:
               result.Summary = $"Candidate matches poorly required Languages, only {matched} matched ";
               break;
           case < 1.0:
               result.Summary = $"Candidate matches greatly required Languages, {matched} languages matched , but not all";
               break;
           case 1.0:
               result.Summary = $"Candidate matches perfectly required Languages, all languages matched";
               break;
       }

       return result;
    }

    private ExperienceStatistics CalculateEducationStatistics(
        EducationRequirement requirement,
        Education candidate)
    {
        var result = new ExperienceStatistics();
        var candidateType = (int)candidate.EducationType;
        var requiredType = (int)requirement.EducationType;
        var candidateDegree = (int)candidate.Degree!;
        var requiredDegree = (int)requirement.Degree!;

        if (candidateType < requiredType)
        {
            result.ExperienceMatchPercent = 0.0;
            result.ExperienceSummary =
                $"Candidate education is critically below required: {candidate.EducationType} ({candidate.Degree}) vs required {requirement.EducationType}.";
            return result;
        }

        if (candidate.EducationType != EducationType.University)
        {
            result.ExperienceMatchPercent = 1.0;
            result.ExperienceSummary =
                $"Candidate perfectly matches education requirement ({candidate.EducationType}).";
            return result;
        }

        if (candidateDegree < requiredDegree)
        {
            result.ExperienceMatchPercent = 0.5;
            result.ExperienceSummary =
                $"Candidate education is below required: {candidate.EducationType} ({candidate.Degree}) vs required {requirement.EducationType} ({requirement.Degree}).";
            return result;
        }

        if (candidateDegree == requiredDegree)
        {
            result.ExperienceMatchPercent = 1.0;
            result.ExperienceSummary =
                $"Candidate perfectly matches education requirement ({candidate.EducationType}, {candidate.Degree}).";
            return result;
        }

        result.ExperienceMatchPercent = 1.5;
        result.ExperienceSummary =
            $"Candidate exceeds education requirement: {candidate.EducationType} ({candidate.Degree}) vs required {requirement.EducationType} ({requirement.Degree}).";
        return result;
    }
}