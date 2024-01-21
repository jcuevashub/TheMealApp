using Email.Core.Dtos;

namespace Email.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage message);
}
