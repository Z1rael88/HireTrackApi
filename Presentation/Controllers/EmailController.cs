using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("emails/")]
public class EmailController(IEmailService emailService) : ControllerBase
{
    [HttpPost("sendGenericEmail")]
    public async Task<IActionResult> SendEmailAsync(string to, string subject, string body)
    {
        await emailService.SendEmailAsync(to,subject, body);
        return Ok();
    } 
}