using Auth.Application.Interfaces.Repositories;
using Auth.Infrastructure.Context;
using Auth.Infrastructure.Repositories;
using Auth.Infrastructure.Repositories.UserRepo;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Auth.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("PostgresConnection");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresConnectionString);
        var dataSource = dataSourceBuilder.Build();


        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(dataSource,
             x => x.MigrationsAssembly("Auth.Infrastructure")));

        services.AddMassTransit(x =>
        {
            // Add outbox
            //x.AddEntityFrameworkOutbox<DataContext>(outbox =>
            //{
            //    outbox.QueryDelay = TimeSpan.FromSeconds(10);

            //    outbox.UseSqlServer();
            //    outbox.UseBusOutbox();
            //});

            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auth", false));

            // Setup RabbitMQ Endpoint
            x.UsingRabbitMq((context, cfg) =>
            {

                cfg.Host(configuration["RabbitMq:Host"], "/", host =>
                {
                    host.Username(configuration["RabbitMq:Username"]);
                    host.Password(configuration["RabbitMq:Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddDbContext<DataContext>(options =>
        {
            options.EnableSensitiveDataLogging();

        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();
        services.AddTransient<IUserTokenRepositoryAsync, UserTokenRepositoryAsync>();
    }
}