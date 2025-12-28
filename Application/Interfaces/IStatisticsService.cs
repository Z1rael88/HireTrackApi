using Application.Dtos.Statistics;

namespace Application.Interfaces;

public interface IStatisticsService
{
    Task<StatisticsResponseDto> GenerateStatisticsForResumeAsync(int vacancyId, int resumeId);
    Task<StatisticsResponseDto> GetStatisticsByResumeIdAsync(int resumeId);
    Task<IEnumerable<StatisticsResponseDto>> GetAllStatisticsByVacancyIdAsync(int vacancyId);

}