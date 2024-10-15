using System.Data;
using Motoca.API.Application.Bikes.Models;
using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class ChangeLicensePlateBikeCommandHandler(ILogger<ChangeLicensePlateBikeCommandHandler> logger,
                                                  IBikesRepository repository) : IRequestHandler<ChangeLicensePlateBikeCommand, Bike>
{
    public async Task<Bike> Handle(ChangeLicensePlateBikeCommand request, CancellationToken cancellationToken)
    {
        var hasAnyBikeWithLicensePlate = await repository.HasAnyBikeWithLicensePlate(request.LicensePlate);

        if (hasAnyBikeWithLicensePlate)
        {
            logger.LogInformation("Já existe uma moto com a placa: {@licensePlate}", request.LicensePlate);
            throw new ConstraintException($"Já existe uma moto com a placa: {request.LicensePlate}");
        }

        var bikeNewLicensePlateEntity = await repository.GetBikeByIdAsync(request.Id);
        bikeNewLicensePlateEntity.SetLicensePlate(request.LicensePlate);

        logger.LogInformation("Alterando a placa da moto: {@bike}", request);

        _ = await repository.UpdateLicensePlateAsync(bikeNewLicensePlateEntity);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Bike(bikeNewLicensePlateEntity.Id,
                        bikeNewLicensePlateEntity.Year,
                        bikeNewLicensePlateEntity.Model,
                        bikeNewLicensePlateEntity.LicensePlate);
    }
}