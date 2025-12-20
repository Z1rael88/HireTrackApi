using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("emails/")]
public class EmailController(IEmailService emailService) : ControllerBase
{
    [Authorize(Roles = nameof(Role.HrManager))]
    [HttpPost("sendGenericEmail")]
    public async Task<IActionResult> SendEmailAsync(string to, string subject, string body)
    {
        await emailService.SendEmailAsync(to,subject, body);
        return Ok();
    } 
}