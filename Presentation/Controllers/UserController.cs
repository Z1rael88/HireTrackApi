using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("users/")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserProfile(int userId)
    {
        var result = await userService.GetUserProfileById(userId);
        return Ok(result);
    }
}