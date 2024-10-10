using Motoca.API.Application.IoC;
using Motoca.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApplicationDependency();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adiciona os endpoints da API
app.MapBikesEndpoints();
app.MapRentalsEndpoints();
app.MapRidersEndpoints();

app.UseHttpsRedirection();
await app.RunAsync();