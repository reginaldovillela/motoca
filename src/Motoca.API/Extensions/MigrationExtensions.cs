using Microsoft.EntityFrameworkCore;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Infrastructure.Bikes;
using Motoca.Infrastructure.Rentals;
using Motoca.Infrastructure.Riders;

namespace Motoca.API.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        #region "bikes"

        using var bikesContext = scope.ServiceProvider.GetRequiredService<BikesContext>();
        bikesContext.Database.Migrate();

        try
        {
            bikesContext.Database.EnsureCreated();

            if (bikesContext.Bikes.FirstOrDefault() is null)
            {
                var bike = new BikeEntity("moto-exemplo");
                bike.SetYear(2024);
                bike.SetModel("modelo-moto-exemplo");
                bike.SetLicensePlate("XXX0X00");

                bikesContext.Bikes.AddRange(bike);

                bikesContext.SaveChanges();
            }
        }
        catch
        {
            // does nothing
        }

        #endregion

        #region "rentals"

        using var rentalsContext = scope.ServiceProvider.GetRequiredService<RentalsContext>();
        rentalsContext.Database.Migrate();

        try
        {
            rentalsContext.Database.EnsureCreated();

            var plans = new PlanEntity[]
            {
                new("plano7", 7, 30, 20),
                new("plano15", 15, 28, 40),
                new("plano30", 30, 22, 0),
                new("plano45", 45, 20, 0),
                new("plano50", 50, 18, 0)
            };

            var rental = new RentalEntity("entregador-exemplo", "moto-exemplo", plans[0]);

            if (rentalsContext.Plans.FirstOrDefault() is null)
            {
                rentalsContext.Plans.AddRange(plans);
            }

            if (rentalsContext.Rentals.FirstOrDefault() is null)
            {
                rentalsContext.Rentals.AddRange(rental);
            }

            rentalsContext.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine("REGINALDO " + ex.Message);
            // does nothing
        }

        #endregion

        #region "riders"

        using var ridersContext = scope.ServiceProvider.GetRequiredService<RidersContext>();
        ridersContext.Database.Migrate();

        try
        {
            ridersContext.Database.EnsureCreated();

            var birthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-20));
            var rider = new RiderEntity("entregador-exemplo", "Fulano de Tal", birthDate);
            rider.SetSocialId("12345678900");
            rider.SetDriversLicense("1234567890", "A");

            if (ridersContext.Riders.FirstOrDefault() is null)
            {
                ridersContext.Riders.AddRange(rider);
            }

            ridersContext.SaveChanges();
        }
        catch
        {
            // does nothing
        }

        #endregion
    }
}
