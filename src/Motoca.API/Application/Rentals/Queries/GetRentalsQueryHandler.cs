using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Rentals.Queries;

#pragma warning disable 1591
public class GetRentalsQueryHandler(ILogger<GetRentalsQueryHandler> logger,
                                    IRentalsRepository repository) : IRequestHandler<GetRentalsQuery, ICollection<Rental>>
{
    public async Task<ICollection<Rental>> Handle(GetRentalsQuery request, CancellationToken cancellationToken)
    {
        var rentals = await repository.GetAllAsync(cancellationToken);

        logger.LogInformation("Consulta concluída. Total de {@Count} encontrados", rentals.Count);

        return rentals.Select(r =>
        {
            r.Recalculate();

            return new Rental(r.EntityId,
                              r.Id,
                              r.RiderId,
                              r.BikeId,
                              new Plan(r.Plan.EntityId,
                                       r.Plan.Id,
                                       r.Plan.DurationTime,
                                       r.Plan.ValuePerDay,
                                       r.Plan.PenaltyPercent),
                              r.CreateAt,
                              r.StartDate,
                              r.ExpectedEndDate,
                              r.ReturnDate,
                              r.AmountToPay,
                              r.IsActive,
                              r.IsOverDue,
                              r.DaysInRental,
                              r.DaysOverDue);
        }).ToList();
    }
}
