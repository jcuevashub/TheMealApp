using Email.Infrastructure.EventBusConsumer;
using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Email.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("PostgresConnection");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresConnectionString);
        var dataSource = dataSourceBuilder.Build();


        //services.AddDbContext<DataContext>(options =>
        //    options.UseNpgsql(dataSource,
        //     x => x.MigrationsAssembly("Auth.Infrastructure")));

        services.AddHangfire(config => config.UsePostgreSqlStorage(postgresConnectionString));
        services.AddHangfireServer();

        services.AddMassTransit(config =>
        {
            config.AddConsumer<UserCreatedConsumer>();
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("email", false));

            // Setup RabbitMQ Endpoint
            config.UsingRabbitMq((context, cfg) =>
            {

                cfg.Host(configuration["RabbitMq:Host"], "/", host =>
                {
                    host.Username(configuration["RabbitMq:Username"]);
                    host.Password(configuration["RabbitMq:Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

    }
}