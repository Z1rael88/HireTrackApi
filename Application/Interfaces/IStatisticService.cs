using Application.Dtos.Statistics;

namespace Application.Interfaces;

public interface IStatisticService
{
    Task<StatisticResponseDto> GenerateStatisticsForResume(int vacancyId, int resumeId);
}