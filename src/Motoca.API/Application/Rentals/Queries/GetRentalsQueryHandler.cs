using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Rentals.Queries;

#pragma warning disable 1591
public class GetRentalsQueryHandler(ILogger<GetRentalsQueryHandler> logger,
                                    IRentalsRepository repository) : IRequestHandler<GetRentalsQuery, Rental[]>
{
    public async Task<Rental[]> Handle(GetRentalsQuery request, CancellationToken cancellationToken)
    {
        var rentals = await repository.GetAllAsync();

        logger.LogInformation("Consulta concluída. Total de {@count} encontrados", rentals.Length);

        return rentals.Select(p => new Rental(p.EntityId,
                                              p.Id,
                                              p.RiderId,
                                              p.BikeId,
                                              new Plan(p.Plan.EntityId,
                                                      p.Plan.Id,
                                                      p.Plan.DefaultDuration,
                                                      p.Plan.ValuePerDay),
                                              p.CreateAt,
                                              p.StartDate,
                                              p.ExpectedEndDate,
                                              p.ReturnDate))
                    .ToArray();
    }
}
