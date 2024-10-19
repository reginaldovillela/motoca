using Motoca.API.Application.Bikes.Models;
using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.API.Application.Bikes.Queries;

#pragma warning disable 1591
public class GetBikesQueryHandler(ILogger<GetBikesQueryHandler> logger,
                                  IBikesRepository repository) : IRequestHandler<GetBikesQuery, Bike[]>
{
    public async Task<Bike[]> Handle(GetBikesQuery request, CancellationToken cancellationToken)
    {
        var bikes = await repository.GetAllAsync(request.LicensePlate);

        logger.LogInformation("Consulta concluída. Total de {@count} encontrados", bikes.Length);

        return bikes.Select(b => new Bike(b.EntityId,
                                          b.Id,
                                          b.Year,
                                          b.Model,
                                          b.LicensePlate))
                    .ToArray();
    }
}
