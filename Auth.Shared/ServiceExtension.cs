
using Auth.Application.Interfaces.Services;
using Auth.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.Shared;

public static class ServiceExtension
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IPasswordService, PasswordService>();
    }

}
