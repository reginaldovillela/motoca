using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Motoca.API.Services.Bikes;
using Motoca.API.Services.Rentals;
using Motoca.API.Services.Riders;
using Motoca.SharedKernel.Message;
using System.Reflection;

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

        var config = builder.Configuration;

        // mediatr
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<Program>();
        });

        services.AddValidatorsFromAssemblyContaining<Program>();

        // masstransit
        services.AddMassTransit(bus =>
        {
            bus.SetKebabCaseEndpointNameFormatter();

            bus.AddConsumer<GetBikeByIdConsumer>().Endpoint(e => e.Name = "get-bike-by-id");
            bus.AddRequestClient<GetBikeByIdRequest>(new Uri("exchange:get-bike-by-id"));

            bus.AddConsumer<GetRiderByIdConsumer>().Endpoint(e => e.Name = "get-rider-by-id");
            bus.AddRequestClient<GetRiderByIdRequest>(new Uri("exchange:get-rider-by-id"));

            bus.AddConsumer<GetBikeHasRentalsConsumer>().Endpoint(e => e.Name = "get-bike-has-rentals");
            bus.AddRequestClient<GetBikeHasRentalsRequest>(new Uri("exchange:get-bike-has-rentals"));

            bus.UsingRabbitMq((context, brokerConfiguration) =>
            {
                brokerConfiguration.Host(config["broker:host"], "/", h =>
                {
                    h.Username(config["broker:user"]!);
                    h.Password(config["broker:pass"]!);
                });

                brokerConfiguration.ConfigureEndpoints(context);
            });
        });
    }
}
