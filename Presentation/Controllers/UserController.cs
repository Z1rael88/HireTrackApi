using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("users/")]
public class UserController(IUserService userService) : ControllerBase
{
    [Authorize(Roles = $"{nameof(Role.HrManager)},{nameof(Role.Candidate)}")]
    [HttpGet("byUserId/{userId}")]
    public async Task<IActionResult> GetUserProfile(int userId)
    {
        var result = await userService.GetUserProfileById(userId);
        return Ok(result);
    }

    [HttpGet("byCandidateId/{candidateId}")]
    public async Task<IActionResult> GetUserByCandidateId(int candidateId)
    {
        var result = await userService.GetUserByCandidateId(candidateId);
        return Ok(result);
    }
}