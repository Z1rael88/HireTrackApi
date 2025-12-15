using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("statistics/")]
public class StatisticsController(IStatisticsService statisticsService) : ControllerBase
{
    [HttpGet("{vacancyId}")]
    public async Task<IActionResult> GetOverallStatisticsByVacancyId(int vacancyId)
    {
        return Ok();
    }

    [HttpGet("{vacancyId}/{resumeId}")]
    public async Task<IActionResult> GetStatisticsByVacancyIdAndResumeId(int vacancyId, int resumeId)
    {
        return Ok();
    }

    [HttpPost("generateStatisticsBy/{resumeId}")]
    public async Task<IActionResult> GenerateStatisticsForResume(int vacancyId, int resumeId)
    {
        var result = await statisticsService.GenerateStatisticsForResumeAsync(vacancyId, resumeId);
        return Ok(result);
    }

    [HttpPost("generateOverallStatisticsBy/{vacancyId}")]
    public async Task<IActionResult> GenerateOverallStatisticsForVacancy(int vacancyId)
    {
        var result = await statisticsService.GetAllStatisticsByVacancyIdAsync(vacancyId);
        return Ok(result);
    }
}