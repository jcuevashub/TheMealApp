using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Email.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}