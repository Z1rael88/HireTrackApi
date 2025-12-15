using Application.Dtos.Statistics;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;
using LanguageLevel = Domain.Models.LanguageLevel;
using StatisticsSummary = Domain.Models.StatisticsSummary;

namespace Application.Services;

public class StatisticsService(IUnitOfWork unitOfWork) : IStatisticsService
{
    private IVacancyRepository _vacancyRepository = unitOfWork.Vacancies;
    private IResumeRepository _resumeRepository = unitOfWork.Resumes;
    private IStatisticsRepository _statisticsRepository = unitOfWork.Statistics;
    private IRepository<Statistics> _statisticsCommonRepository = unitOfWork.Repository<Statistics>();

    public async Task<StatisticsResponseDto> GenerateStatisticsForResumeAsync(int vacancyId, int resumeId)
    {
        var resume = await _resumeRepository.GetResumeById(resumeId);
        var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);

        if (resume is null || vacancy is null)
        {
            throw new NotFoundException("Vacancy and Resume not found");
        }

        var statisticsResult = new Statistics();
        statisticsResult.VacancyId = vacancyId;
        statisticsResult.ResumeId = resumeId;

        var education = resume.Educations.FirstOrDefault(); 
        var educationStatistics = CalculateEducationStatistics(vacancy.EducationsRequirement, education);
        statisticsResult.EducationMatchPercent = educationStatistics.EducationMatchPercent;
        statisticsResult.EducationSummary = educationStatistics.EducationSummary;

        var languageLevelStatistics =
            CalculateLanguageLevelStatistics(vacancy.LanguageLevelRequirements, resume.LanguageLevels);
        statisticsResult.LanguageSummary = languageLevelStatistics.LanguageLevelSummary;
        statisticsResult.LanguageMatchPercent = languageLevelStatistics.MatchPercent;

        var experienceStatistics =
            CalculateJobExperienceStatistics(vacancy.JobExperienceRequirement, resume.JobExperiences);
        statisticsResult.ExperienceSummary = experienceStatistics.ExperienceSummary;
        statisticsResult.ExperienceMatchPercent = experienceStatistics.ExperienceMatchPercent;

        var totalStatistics =
            CalculateTotalStatistics(experienceStatistics, languageLevelStatistics, educationStatistics, vacancy,resume);
        statisticsResult.Summary = totalStatistics.SummaryDto.Adapt<StatisticsSummary>();
        statisticsResult.TotalMatchPercent = totalStatistics.TotalMatchPercent;
        
        var createdStatistics = await _statisticsCommonRepository.CreateAsync(statisticsResult);
        return statisticsResult.Adapt<StatisticsResponseDto>();
    }

    public async Task<StatisticsResponseDto> GetStatisticsByIdAsync(int statisticsId)
    {
        var statistics = await _statisticsCommonRepository.GetByIdAsync(statisticsId);
        return statistics.Adapt<StatisticsResponseDto>();
    }

    public async Task<IEnumerable<StatisticsResponseDto>> GetAllStatisticsByVacancyIdAsync(int vacancyId)
    {
        var statistics = await _statisticsRepository.GetAllStatisticsByVacancyId(vacancyId);
        return statistics.Adapt<IEnumerable<StatisticsResponseDto>>();
    }

    private TotalStatistics CalculateTotalStatistics(ExperienceStatistics experienceStatistics,
        LanguageLevelStatistics languageLevelStatistics, EducationStatistics educationStatistics,Vacancy vacancy, Resume resume)
    {
        var result = new TotalStatistics();
        var workTypeMatches = resume.Candidate.WorkType
            .Any(wt => vacancy.WorkType.Contains(wt));
        if (!workTypeMatches)
        {
            result.TotalMatchPercent = 0;
            result.SummaryDto = new StatisticsSummaryDto
            {
                EducationSummary = educationStatistics.EducationSummary,
                ExperienceSummary = experienceStatistics.ExperienceSummary,
                LanguageLevelSummary = languageLevelStatistics.LanguageLevelSummary,
                TotalSummary = "Candidate work type does not match the vacancy requirements"
            };

            return result;
        }

        var totalPercent = (languageLevelStatistics.MatchPercent + experienceStatistics.ExperienceMatchPercent +
                            educationStatistics.EducationMatchPercent) / 3;

        result.TotalMatchPercent = totalPercent;
        result.SummaryDto = new StatisticsSummaryDto
        {
            EducationSummary = educationStatistics.EducationSummary,
            ExperienceSummary = experienceStatistics.ExperienceSummary,
            LanguageLevelSummary = languageLevelStatistics.LanguageLevelSummary,
            TotalSummary = result.TotalMatchPercent switch
            {
                0.0 => "Unfortunately candidate doesnt match any of the crucial parameters",
                < 0.5 =>
                    "Candidate matches some of the crucial parameters, you can check summary for each of the parameters to decide",
                < 1.0 =>
                    "Candidate matches most of the crucial parameters,you can check summary for each of the parameters to decide. We would recommend this candidate for an interview",
                _ => "Error"
            }
        };

        return result;
    }

    private ExperienceStatistics CalculateJobExperienceStatistics(
        JobExperienceRequirement requirement,
        IEnumerable<JobExperience> jobExperiences)
    {
        var list = jobExperiences.ToList();

        if (!list.Any())
            return null;

        if (list.Count == 1)
            return CalculateSingleJobExperience(requirement, list[0]);

        var results = list
            .Select(exp => CalculateSingleJobExperience(requirement, exp))
            .ToList();

        return new ExperienceStatistics
        {
            ExperienceMatchPercent = results.Average(r => r.ExperienceMatchPercent),
            ExperienceSummary = "Aggregated experience across multiple roles."
        };
    }
    private ExperienceStatistics CalculateSingleJobExperience(
        JobExperienceRequirement requirement,
        JobExperience experience)
    {
        var result = new ExperienceStatistics();

        var required = requirement.TechnologyRequirements.ToList();
        var provided = experience.Technologies.ToList();

        var matchedPairs = required.Join(
            provided,
            req => req.TechnologyType.Name,
            prov => prov.TechnologyType.Name,
            (req, prov) => new { Required = req, Provided = prov }
        ).ToList();

        if (!matchedPairs.Any())
        {
            result.ExperienceMatchPercent = 0.0;
            result.ExperienceSummary = "No required technology experiences specified";
            return result;
        }

        var partialScores = matchedPairs.Select(x =>
        {
            double reqYears = x.Required.YearsOfExperience;
            double provYears = x.Provided.YearsOfExperience;

            if (reqYears <= 0)
                return 1.25;

            double ratio = provYears / reqYears;
            return Math.Min(ratio, 1.25);
        }).ToList();

        result.ExperienceMatchPercent = partialScores.Average();
        result.ExperienceSummary = result.ExperienceMatchPercent switch
        {
            0.0 => "Candidate does not match required experience.",
            < 0.5 => "Candidate meets experience requirements poorly.",
            < 1.0 => "Candidate meets most experience requirements.",
            1.0 => "Candidate fully meets all experience requirements.",
            > 1.0 => "Candidate exceeds experience requirements.",
            _ => "Unexpected result"
        };

        return result;
    }


    private LanguageLevelStatistics CalculateLanguageLevelStatistics(
        ICollection<LanguageLevelRequirement> languageLevelRequirements,
        ICollection<LanguageLevel> resumeLanguageLevels)
    {
        var result = new LanguageLevelStatistics();

        var required = languageLevelRequirements.ToList();
        var provided = resumeLanguageLevels.ToList();

        var totalRequired = required.Count;

        if (totalRequired == 0)
        {
            result.MatchPercent = 0.0;
            result.LanguageLevelSummary = "No required languages specified";
            return result;
        }

        var matched = required.Count(req =>
            provided.Any(c => c.Language == req.Language && c.Level == req.Level));

        result.MatchPercent = (double)matched / totalRequired;

        result.LanguageLevelSummary = result.MatchPercent switch
        {
            0.0 => "Candidate does not match required languages.",
            < 0.5 => $"Candidate matches poorly: only {matched} languages matched.",
            < 1.0 => $"Candidate matches well: {matched} languages matched (not all).",
            1.0 => "Candidate matches all required languages perfectly.",
            _ => result.LanguageLevelSummary
        };

        return result;
    }


    private EducationStatistics CalculateEducationStatistics(
        EducationRequirement requirement,
        Education candidate)
    {
        var result = new EducationStatistics();
        var candidateType = (int)candidate.EducationType;
        var requiredType = (int)requirement.EducationType;
        var candidateDegree = (int)candidate.Degree!;
        var requiredDegree = (int)requirement.Degree!;

        if (candidateType < requiredType)
        {
            result.EducationMatchPercent = 0.0;
            result.EducationSummary =
                $"Candidate education is critically below required: {candidate.EducationType} ({candidate.Degree}) vs required {requirement.EducationType}.";
            return result;
        }

        if (candidate.EducationType != EducationType.University)
        {
            result.EducationMatchPercent = 1.0;
            result.EducationSummary =
                $"Candidate perfectly matches education requirement ({candidate.EducationType}).";
            return result;
        }

        if (candidateDegree < requiredDegree)
        {
            result.EducationMatchPercent = 0.5;
            result.EducationSummary =
                $"Candidate education is below required: {candidate.EducationType} ({candidate.Degree}) vs required {requirement.EducationType} ({requirement.Degree}).";
            return result;
        }

        if (candidateDegree == requiredDegree)
        {
            result.EducationMatchPercent = 1.0;
            result.EducationSummary =
                $"Candidate perfectly matches education requirement ({candidate.EducationType}, {candidate.Degree}).";
            return result;
        }

        result.EducationMatchPercent = 1.5;
        result.EducationSummary =
            $"Candidate exceeds education requirement: {candidate.EducationType} ({candidate.Degree}) vs required {requirement.EducationType} ({requirement.Degree}).";
        return result;
    }
}