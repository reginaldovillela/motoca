using Microsoft.EntityFrameworkCore;
using Motoca.Infrastructure.Bikes;
using Motoca.Infrastructure.Rentals;
using Motoca.Infrastructure.Riders;

namespace Motoca.API.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var bikesContext = scope.ServiceProvider.GetRequiredService<BikesContext>();
        bikesContext.Database.Migrate();
        
        using var rentalsContext = scope.ServiceProvider.GetRequiredService<RentalsContext>();
        rentalsContext.Database.Migrate();

        using var ridersContext = scope.ServiceProvider.GetRequiredService<RidersContext>();
        rentalsContext.Database.Migrate();
    }
}
