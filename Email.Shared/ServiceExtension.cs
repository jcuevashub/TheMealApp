using Email.Application.Interfaces.Services;
using Email.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Email.Shared;

public static class ServiceExtension
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<ISendEmailJob, SendEmailJob>();
        services.AddSingleton<IEmailService>(sp => new MailgunEmailService(configuration["Mailgun:Domain"]!, configuration["Mailgun:ApiKey"]!));


    }

}