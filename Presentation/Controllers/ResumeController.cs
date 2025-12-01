using Application.Dtos.Resume;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
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

    [HttpPatch("changeStatus/{resumeId}")]
    public async Task<IActionResult> ChangeResumeStatus(int resumeId,int vacancyId, ResumeStatus status)
    {
        await resumeService.ChangeStatusOfResumeAsync(resumeId, vacancyId,status);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateResume(ResumeRequestDto resume, int resumeId)
    {
        await resumeService.UpdateResumeAsync(resume, resumeId);
        return Ok();
    }

    [HttpGet("byUserId{userId}")]
    public async Task<IActionResult> GetResumeByUserId(int userId)
    {
       var resume = await resumeService.GetResumeByUserIdAsync(userId);
        return Ok(resume);
    }
}