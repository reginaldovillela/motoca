using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Motoca.API.Endpoints;
using Motoca.API.Extensions;
using Motoca.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddDefaultServices();
builder.AddApplicationServices();
builder.Services.RegisterInfrastructureDependencies(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapHealthChecks("/health");

    app.MapHealthChecks("/alive", new HealthCheckOptions
    {
        Predicate = r => r.Tags.Contains("live")
    });
}

// Adiciona os endpoints da API
app.MapBikesEndpoints();
app.MapRentalsEndpoints();
app.MapRidersEndpoints();

app.UseHttpsRedirection();
await app.RunAsync();