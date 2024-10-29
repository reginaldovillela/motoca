using Microsoft.EntityFrameworkCore;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Infrastructure.Bikes;
using Motoca.Infrastructure.Rentals;
using Motoca.Infrastructure.Riders;
using System.Globalization;

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
                var bike1 = new BikeEntity("moto-IAP5I92");
                bike1.SetYear(2024);
                bike1.SetModel("Modelo Sport");
                bike1.SetLicensePlate("IAP5I92");

                var bike2 = new BikeEntity("moto-HZQ1A69");
                bike2.SetYear(2023);
                bike2.SetModel("Modelo Custom");
                bike2.SetLicensePlate("HZQ1A69");

                var bike3 = new BikeEntity("moto-MVH6G02");
                bike3.SetYear(2012);
                bike3.SetModel("Modelo Street");
                bike3.SetLicensePlate("MVH6G02");

                var bike4 = new BikeEntity("moto-DMX8G02");
                bike4.SetYear(2020);
                bike4.SetModel("Modelo Sport");
                bike4.SetLicensePlate("DMX8G02");

                bikesContext.Bikes.AddRange(bike1, bike2, bike3, bike4);

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

            var rentals = new List<RentalEntity>()
            {
                new("entregador-matheus", "moto-IAP5I92", plans[1], DateTime.UtcNow.AddDays(-4)),
                new("entregador-thales", "moto-MVH6G02", plans[0], DateTime.UtcNow.AddDays(-10)),
                new("entregador-rafaela", "moto-DMX8G02", plans[2], DateTime.UtcNow.AddDays(-15))
            };

            if (rentalsContext.Plans.FirstOrDefault() is null)
            {
                rentalsContext.Plans.AddRange(plans);
            }

            if (rentalsContext.Rentals.FirstOrDefault() is null)
            {
                rentalsContext.Rentals.AddRange(rentals);
            }

            rentalsContext.SaveChanges();
        }
        catch
        {
            // does nothing
        }

        #endregion

        #region "riders"

        using var ridersContext = scope.ServiceProvider.GetRequiredService<RidersContext>();
        ridersContext.Database.Migrate();

        try
        {
            ridersContext.Database.EnsureCreated();

            var format = "dd/MM/yyyy";

            var birthDate1 = DateOnly.ParseExact("09/03/1997", format, CultureInfo.CurrentCulture);
            var rider1 = new RiderEntity("entregador-matheus", "Matheus Rafael Gonçalves", birthDate1);
            rider1.SetSocialId("47343114476");
            rider1.SetDriversLicense("149369049", "A");

            var birthDate2 = DateOnly.ParseExact("09/01/1969", format, CultureInfo.CurrentCulture);
            var rider2 = new RiderEntity("entregador-thales", "Thales Otávio Samuel Baptista", birthDate2);
            rider2.SetSocialId("37516840939");
            rider2.SetDriversLicense("256727594", "A");

            var birthDate3 = DateOnly.ParseExact("09/03/1997", format, CultureInfo.CurrentCulture);
            var rider3 = new RiderEntity("entregador-julio", "Julio Antonio Rodrigo Monteiro", birthDate3);
            rider3.SetSocialId("48924209990");
            rider3.SetDriversLicense("346879334", "B");

            var birthDate4 = DateOnly.ParseExact("03/03/2003", format, CultureInfo.CurrentCulture);
            var rider4 = new RiderEntity("entregador-rafaela", "Rafaela Carolina Maya Mendes", birthDate4);
            rider4.SetSocialId("63661724045");
            rider4.SetDriversLicense("174624074", "A");

            var birthDate5 = DateOnly.ParseExact("24/06/1975", format, CultureInfo.CurrentCulture);
            var rider5 = new RiderEntity("entregador-benicio", "Benício Bento Jesus", birthDate5);
            rider5.SetSocialId("88846905369");
            rider5.SetDriversLicense("361530390", "A");

            if (ridersContext.Riders.FirstOrDefault() is null)
            {
                ridersContext.Riders.AddRange(rider1, rider2, rider3, rider4, rider5);
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
