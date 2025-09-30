using Application.Dtos.Resume;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("resumes/")]
public class ResumeController(IResumeService resumeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateResume(ResumeRequestDto dto)
    {
        var resume = await resumeService.CreateResumeAsync(dto);
        return Ok(resume);
    }

    [HttpGet("{resumeId}")]
    public async Task<IActionResult> GetResumeById(int resumeId)
    {
        var resume = await resumeService.GetResumeByIdAsync(resumeId);
        return Ok(resume);
    }
    [HttpGet("byVacancyId/{vacancyId}")]
    public async Task<IActionResult> GetAllResumesByVacancyId(int vacancyId)
    {
        var resume = await resumeService.GetAllResumesByVacancyIdAsync(vacancyId);
        return Ok(resume);
    }
}