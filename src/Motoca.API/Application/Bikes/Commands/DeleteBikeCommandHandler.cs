using System.Data;
using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class DeleteBikeCommandHandler(ILogger<DeleteBikeCommandHandler> logger,
                                      IBikesRepository repository) : IRequestHandler<DeleteBikeCommand, bool>
{
    public async Task<bool> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
    {
        var bikeToDelete = await repository.GetBikeByIdAsync(request.Id);

        if (bikeToDelete is null)
        {
            logger.LogInformation("Moto com o Id {@Id} não foi encontrada", request.Id);
            throw new ConstraintException($"Moto com o Id {request.Id} não foi encontrada");
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

        return true;
    }
}