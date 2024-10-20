using System.Reflection;
using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Motoca.API.Application.Bikes.Services;
using Motoca.API.Application.Riders.Services;
using Motoca.SharedKernel.Message;

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

        services.AddMassTransit(x =>
        {
            x.AddConsumer<GetBikeByIdConsumer>().Endpoint(e => e.Name = "get-bike-by-id");
            x.AddRequestClient<GetBikeByIdRequest>(new Uri("exchange:get-bike-by-id"));

            x.AddConsumer<GetRiderByIdConsumer>().Endpoint(e => e.Name = "get-rider-by-id");
            x.AddRequestClient<GetRiderByIdRequest>(new Uri("exchange:get-rider-by-id"));

            // x.UsingInMemory((context, cfg) =>
            // {
            //     cfg.ConfigureEndpoints(context);
            // });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("rabbitmq");
                    h.Password("rabbitmq");
                });

                // cfg.Host(new Uri("amqp://localhost:5672"), h =>
                // {
                //     h.Username("rabbitmq");
                //     h.Password("rabbitmq");
                // });

                cfg.ConfigureEndpoints(context);
            });


        });
    }
}
