using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Infrastructure.Bikes;
using Motoca.Infrastructure.Bikes.Repositories;
using Motoca.Infrastructure.Rentals;
using Motoca.Infrastructure.Rentals.Repositories;
using Motoca.Infrastructure.Riders;
using Motoca.Infrastructure.Riders.Repositories;
using Motoca.Infrastructure.Riders.Services;

namespace Motoca.Infrastructure.IoC;

public static class InfrastructureDependecy
{
    public static void RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        /*
         * Bikes
         */

        services.AddDbContext<BikesContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MotocaConnection"));
        });

        services.AddScoped<IBikesRepository, BikesRepository>();
        services.AddScoped<BikesContext>();

        /*
         * Rentals
         */

        services.AddDbContext<RentalsContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MotocaConnection"));
        });

        services.AddScoped<IRentalsRepository, RentalsRepository>();
        services.AddScoped<IPlansRepository, PlansRepository>();
        services.AddScoped<RentalsContext>();

        /*
         * Riders
         */

        services.AddDbContext<RidersContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MotocaConnection"));
        });

        services.AddScoped<IRidersRepository, RidersRepository>();
        services.AddScoped<RidersContext>();

        services.AddScoped<IRidersStorageService, RidersStorageService>();
    }

    

}
