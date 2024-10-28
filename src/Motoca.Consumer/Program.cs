using Microsoft.Extensions.Hosting;
using Motoca.Consumer;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(bus =>
    {
        bus.SetKebabCaseEndpointNameFormatter();

        bus.AddConsumer<BikeHasBeenCreatedMessageConsumer>().Endpoint(e => e.Name = "bike-has-been-created");

        bus.UsingRabbitMq((context, brokerConfiguration) =>
        {
            brokerConfiguration.Host("rabbitmq", "/", h =>
            {
                h.Username("rabbitmq");
                h.Password("rabbitmq");
            });

            brokerConfiguration.ConfigureEndpoints(context);
        });
    });
});

var app = builder.Build();

await app.RunAsync();
