using Email.Application.Interfaces.Services;
using Email.Core.Dtos;

namespace Email.Shared.Services;

public class SendEmailJob : ISendEmailJob
{
    private readonly IEmailService _emailService;

    public SendEmailJob(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task ExecuteAsync(EmailMessage message)
    {
        await _emailService.SendEmailAsync(message);
    }
}
