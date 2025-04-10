using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using FluxStore.Application.Interfaces;
using FluxStore.Infrastructure.Auth;
using Microsoft.Extensions.Logging;

namespace FluxStore.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailService> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("FluxStore", _smtpSettings.Username));
            email.To.Add(new MailboxAddress(to, to));
            email.Subject = subject;

            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
            try
            {
                await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authentication error.");
            }
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}