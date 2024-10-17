using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Infrastructure.Bikes;
using Motoca.Infrastructure.Bikes.Repositories;
using Motoca.Infrastructure.Rentals.Repositories;
using Motoca.Infrastructure.Riders.Repositories;

namespace Motoca.API.Extensions;

internal static class ServiceExtensions
{
    public static void AddDefaultServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services
            .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            config.CustomSchemaIds(x => x.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
                                     ?? x.DefaultSchemaIdSelector());
        });

        services.Configure<ApiBehaviorOptions>(config =>
        {
            config.SuppressInferBindingSourcesForParameters = true;
        });
    }

    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<Program>();
        });

        services.AddValidatorsFromAssemblyContaining<Program>(); //(ServiceLifetime.Singleton);
    }

    public static void AddRepositories(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddDbContext<BikesContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("BikesConnection"));
        });

        services.AddScoped<IBikesRepository, BikesRepository>();
        services.AddScoped<IRentalsRepository, RentalsRepository>();
        services.AddScoped<IPlansRepository, PlansRepository>();
        services.AddScoped<IRidersRepository, RidersRepository>();
    }
}
