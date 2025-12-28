using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("statistics/")]
public class StatisticsController(IStatisticsService statisticsService) : ControllerBase
{
    [Authorize(Roles = nameof(Role.HrManager))]
    [HttpPost("generateStatisticsBy/{resumeId}")]
    public async Task<IActionResult> GenerateStatisticsForResume(int vacancyId, int resumeId)
    {
        var result = await statisticsService.GenerateStatisticsForResumeAsync(vacancyId, resumeId);
        return Ok(result);
    }
    [Authorize(Roles = nameof(Role.HrManager))]
    [HttpGet("getOverallStatisticsBy/{vacancyId}")]
    public async Task<IActionResult> GetOverallStatisticsForVacancy(int vacancyId)
    {
        var result = await statisticsService.GetAllStatisticsByVacancyIdAsync(vacancyId);
        return Ok(result);
    }
    [Authorize(Roles = nameof(Role.HrManager))]
    [HttpGet("getStatisticsBy/{resumeId}")]
    public async Task<IActionResult> GetStatisticsForResume(int resumeId)
    {
        var result = await statisticsService.GetStatisticsByResumeIdAsync(resumeId);
        return Ok(result);
    }
}