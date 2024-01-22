using System.Net;
using System.Net.Mail;
using Email.Application.Interfaces.Services;
using Email.Core.Dtos;
using Microsoft.Extensions.Options;


namespace Email.Shared;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(EmailMessage message)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.Sender, _emailSettings.SenderName),
            Subject = message.Subject,
            Body = message.Body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(new MailAddress(message.To));

        using var smtpClient = new SmtpClient(_emailSettings.MailServer, _emailSettings.MailPort)
        {
            Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}

public class EmailSettings
{
    public string MailServer { get; set; }
    public int MailPort { get; set; }
    public string SenderName { get; set; }
    public string Sender { get; set; }
    public string Password { get; set; }
}
