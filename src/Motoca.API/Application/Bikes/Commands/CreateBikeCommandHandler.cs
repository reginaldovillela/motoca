using System.Data;
using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class CreateBikeCommandHandler(ILogger<CreateBikeCommandHandler> logger,
                                      IBikesRepository repository) : IRequestHandler<CreateBikeCommand, Bike>
{
    public async Task<Bike> Handle(CreateBikeCommand request, CancellationToken cancellationToken)
    {
        // verifica se já existe uma moto cadastrada com o mesmo Id
        var hasAnyBikeWithId = await repository.HasAnyBikeWithId(request.Id);

        if (hasAnyBikeWithId)
        {
            logger.LogInformation("Já existe uma moto com o Id: {@Id}", request.Id);
            throw new ConstraintException($"Já existe uma moto com o Id: {request.Id}");
        }

        // verifica se placa digitada é diferente da atual
        var hasAnyBikeWithLicensePlate = await repository.HasAnyBikeWithLicensePlate(request.LicensePlate);

        if (hasAnyBikeWithLicensePlate)
        {
            logger.LogInformation("Já existe uma moto com a placa: {@LicensePlate}", request.LicensePlate);
            throw new ConstraintException($"Já existe uma moto com a placa: {request.LicensePlate}");
        }

        var newBike = new BikeEntity(request.Id);
        newBike.SetYear((ushort)request.Year);
        newBike.SetModel(request.Model);
        newBike.SetLicensePlate(request.LicensePlate);

        _ = await repository.AddAsync(newBike);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Criando o registro da moto: {@bike}", request);

        return new Bike(newBike.EntityId,
                        newBike.Id,
                        newBike.Year,
                        newBike.Model,
                        newBike.LicensePlate);
    }
}