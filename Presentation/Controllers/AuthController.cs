using Application.Dtos.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("auth/")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var result = await authService.Register(dto);
        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var result = await authService.Login(dto);
        return Ok(result);
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(string refreshToken)
    {
        var result = await authService.Refresh(refreshToken);
        return Ok(result);
    }
}