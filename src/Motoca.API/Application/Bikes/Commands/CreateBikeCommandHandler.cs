using System.Data;
using Motoca.API.Application.Bikes.Models;
using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class CreateBikeCommandHandler(ILogger<CreateBikeCommandHandler> logger,
                                      IBikesRepository repository) : IRequestHandler<CreateBikeCommand, Bike>
{
    public async Task<Bike> Handle(CreateBikeCommand request, CancellationToken cancellationToken)
    {
        var hasAnyBikeWithLicensePlate = await repository.HasAnyBikeWithLicensePlate(request.LicensePlate);

        if (hasAnyBikeWithLicensePlate)
        {
            logger.LogInformation("Já existe uma moto com a placa: {@licensePlate}", request.LicensePlate);
            throw new ConstraintException($"Já existe uma moto com a placa: {request.LicensePlate}");
        }

        var newBikeEntity = new BikeEntity(request.Id);
        newBikeEntity.SetYear((ushort)request.Year);
        newBikeEntity.SetModel(request.Model);
        newBikeEntity.SetLicensePlate(request.LicensePlate);

        logger.LogInformation("Criando o registro da moto: {@bike}", request);

        _ = await repository.AddAsync(newBikeEntity);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Bike(newBikeEntity.Id,
                        newBikeEntity.Year,
                        newBikeEntity.Model,
                        newBikeEntity.LicensePlate);
    }
}