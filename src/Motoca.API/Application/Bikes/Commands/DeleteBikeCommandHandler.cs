using System.Data;
using Motoca.API.Application.Bikes.Models;
using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class DeleteBikeCommandHandler(ILogger<DeleteBikeCommandHandler> logger,
                                      IBikesRepository repository) : IRequestHandler<DeleteBikeCommand, Bike?>
{
    public async Task<Bike?> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
    {
        var bikeToDelete = await repository.GetByIdAsync(request.Id);

        if (bikeToDelete is null)
        {
            logger.LogInformation("Não foi encontrada a moto com o Id: {@Id}", request.Id);
            return null;
        }

        //Todo: Já o serviço de locações

        var hasAnyRentals = false;

        if (hasAnyRentals)
        {
            logger.LogInformation("Moto com o Id {@Id} já possui locações e não pode ser removida", request.Id);
            throw new ConstraintException($"Moto com o Id {request.Id} já possui locações e não pode ser removida");
        }

        logger.LogInformation("Deletando a moto: {@bike}", bikeToDelete);

        _ = await repository.DeleteAsync(bikeToDelete);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Bike(bikeToDelete.EntityId,
                        bikeToDelete.Id,
                        bikeToDelete.Year,
                        bikeToDelete.Model,
                        bikeToDelete.LicensePlate);
    }
}