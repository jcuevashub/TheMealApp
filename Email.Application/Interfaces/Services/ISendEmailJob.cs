using Email.Core.Dtos;

namespace Email.Application.Interfaces.Services;

public interface ISendEmailJob
{
    Task ExecuteAsync(EmailMessage message);
}
