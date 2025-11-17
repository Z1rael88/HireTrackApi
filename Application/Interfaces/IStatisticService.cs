using Application.Dtos.Statistics;

namespace Application.Interfaces;

public interface IStatisticService
{
    Task<StatisticResponseDto> GenerateStatisticsAsync(int vacancyId, int resumeId);
}