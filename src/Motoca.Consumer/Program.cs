using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Motoca.SharedKernel.Message;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(bus =>
    {
        bus.SetKebabCaseEndpointNameFormatter();

        bus.AddConsumer<BikeHasBeenCreatedMessage>().Endpoint(e => e.Name = "bike-has-been-created");

        bus.UsingRabbitMq((context, brokerConfiguration) =>
        {
            brokerConfiguration.Host("localhost", "/", h =>
            {
                h.Username("rabbitmq");
                h.Password("rabbitmq");
            });

            brokerConfiguration.ConfigureEndpoints(context);
        });
    });
});

var app = builder.Build();

app.Run();