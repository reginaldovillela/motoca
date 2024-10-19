using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Infrastructure.Bikes;
using Motoca.Infrastructure.Bikes.Repositories;
using Motoca.Infrastructure.Rentals.Repositories;
using Motoca.Infrastructure.Riders.Repositories;

namespace Motoca.Infrastructure.IoC;

public static class InfrastructureDependecy
{
    public static void RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BikesContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MotocaConnection"));
        });

        services.AddScoped<IBikesRepository, BikesRepository>();
        services.AddScoped<IRentalsRepository, RentalsRepository>();
        services.AddScoped<IPlansRepository, PlansRepository>();
        services.AddScoped<IRidersRepository, RidersRepository>();

        // adiciona os contexts
        services.AddScoped<BikesContext>();
    }

}
