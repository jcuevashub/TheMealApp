using Catalog.Application.Interfaces.Services;
using Catalog.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Shared;

public static class ServiceExtension
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<ITheMealService, TheMealService>();
    }

}