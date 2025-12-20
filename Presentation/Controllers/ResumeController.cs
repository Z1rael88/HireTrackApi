using Application.Dtos.Resume;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = nameof(Role.HrManager))]
    [HttpGet("{resumeId}")]
    public async Task<IActionResult> GetResumeById(int resumeId)
    {
        var resume = await resumeService.GetResumeByIdAsync(resumeId);
        return Ok(resume);
    }
    [Authorize(Roles = nameof(Role.HrManager))]
    [HttpGet("byVacancyId/{vacancyId}")]
    public async Task<IActionResult> GetAllResumesByVacancyId(int vacancyId)
    {
        var resume = await resumeService.GetAllResumesByVacancyIdAsync(vacancyId);
        return Ok(resume);
    }
    [Authorize(Roles = nameof(Role.HrManager))]
    [HttpPatch("changeStatus/{resumeId}")]
    public async Task<IActionResult> ChangeResumeStatus(int resumeId,int vacancyId, ResumeStatus status)
    {
        await resumeService.ChangeStatusOfResumeAsync(resumeId, vacancyId,status);
        return Ok();
    }
    [Authorize(Roles = nameof(Role.Candidate))]
    [HttpPut]
    public async Task<IActionResult> UpdateResume(ResumeRequestDto resume, int resumeId)
    {
        await resumeService.UpdateResumeAsync(resume, resumeId);
        return Ok();
    }
    [Authorize(Roles = $"{nameof(Role.HrManager)},{nameof(Role.Candidate)}")]
    [HttpGet("byUserId/{userId}")]
    public async Task<IActionResult> GetResumeByUserId(int userId)
    {
       var resume = await resumeService.GetResumeByUserIdAsync(userId);
        return Ok(resume);
    }
    [HttpPost("uploadResume")]
    public IActionResult UploadResume(IFormFile resume)
    {
        var result =  resumeService.UploadResumeAsync(resume);
        return Ok(result);
    }
}