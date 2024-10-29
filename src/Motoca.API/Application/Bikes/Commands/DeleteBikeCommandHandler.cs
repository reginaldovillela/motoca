using MassTransit;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.SharedKernel.Application.Models;
using Motoca.SharedKernel.Message;
using System.Data;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class DeleteBikeCommandHandler(ILogger<DeleteBikeCommandHandler> logger,
                                      IBikesRepository repository,
                                      IRequestClient<GetBikeHasRentalsRequest> bikeRentalsConsumer) : IRequestHandler<DeleteBikeCommand, Bike?>
{
    public async Task<Bike?> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
    {
        var bikeToDelete = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (bikeToDelete is null)
        {
            logger.LogInformation("Não foi encontrada a moto com o Id: {@Id}", request.Id);
            return null;
        }

        // Verificar se a moto possui historico de agendamento
        var getBikeRentalsRequest = new GetBikeHasRentalsRequest { BikeId = request.Id };
        var getBikeRentalsResponse = await bikeRentalsConsumer.GetResponse<GetBikeHasRentalsResponse>(getBikeRentalsRequest, cancellationToken);

        var messageBikeRentals = getBikeRentalsResponse.Message;

        if (messageBikeRentals.HasRentals)
        {
            logger.LogInformation("Moto com o Id {@Id} já possui histórico de locações e não pode ser removida", request.Id);
            throw new ConstraintException($"Moto com o Id {request.Id} já possui histórico locações e não pode ser removida");
        }

        logger.LogInformation("Deletando a moto: {@Bike}", bikeToDelete);

        _ = await repository.DeleteAsync(bikeToDelete, cancellationToken);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Bike(bikeToDelete.EntityId,
                        bikeToDelete.Id,
                        bikeToDelete.Year,
                        bikeToDelete.Model,
                        bikeToDelete.LicensePlate);
    }
}