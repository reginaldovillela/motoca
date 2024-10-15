using System.Data;
using Motoca.API.Application.Bikes.Models;
using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.API.Application.Bikes.Queries;

#pragma warning disable 1591
public class GetBikeByIdQueryHandler(ILogger<GetBikeByIdQueryHandler> logger,
                                     IBikesRepository repository) : IRequestHandler<GetBikeByIdQuery, Bike>
{
    public async Task<Bike> Handle(GetBikeByIdQuery request, CancellationToken cancellationToken)
    {
        var bike = await repository.GetBikeByIdAsync(request.Id);

        if (bike is null)
        {
            logger.LogInformation("Moto com o Id {@Id} não foi encontrada", request.Id);
            throw new ConstraintException($"Moto com o Id {request.Id} não foi encontrada");
        }

        return new Bike(bike.Id,
                        bike.Year,
                        bike.Model,
                        bike.LicensePlate);
    }
}
