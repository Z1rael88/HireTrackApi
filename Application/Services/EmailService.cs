using Application.Interfaces;
using Infrastructure.ValidationOptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Application.Services;

public class EmailService(IOptions<SmtpOptions> configuration) : IEmailService
{
    private readonly SmtpOptions _configuration = configuration.Value;

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("HireTrack",_configuration.From));
        message.To.Add(new MailboxAddress("To", to));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_configuration.Host, _configuration.Port, _configuration.UseSSL);
        await client.AuthenticateAsync(_configuration.Username, _configuration.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}