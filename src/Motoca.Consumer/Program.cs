using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Motoca.Consumer;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddLogging(log =>
    {
        log.AddFilter("Microsoft", LogLevel.Warning)
           .AddFilter("System", LogLevel.Warning)
           .AddConsole();
    });

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

Console.Clear();
Console.WriteLine("### Consumer aguardando novos cadastros de motos!! ###");

await app.RunAsync();
