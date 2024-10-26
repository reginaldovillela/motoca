using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Bikes.Queries;

#pragma warning disable 1591
public class GetBikeByIdQueryHandler(ILogger<GetBikeByIdQueryHandler> logger,
                                     IBikesRepository repository) : IRequestHandler<GetBikeByIdQuery, Bike?>
{
    public async Task<Bike?> Handle(GetBikeByIdQuery request, CancellationToken cancellationToken)
    {
        var bike = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (bike is null)
        {
            logger.LogInformation("Não foi encontrada a moto com o Id: {@Id}", request.Id);
            return null;
        }

        return new Bike(bike.EntityId,
                        bike.Id,
                        bike.Year,
                        bike.Model,
                        bike.LicensePlate);
    }
}
