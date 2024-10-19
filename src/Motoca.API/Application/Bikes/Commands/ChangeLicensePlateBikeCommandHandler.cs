using System.Data;
using Motoca.API.Application.Bikes.Models;
using Motoca.Domain.Bikes.AggregatesModel;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class ChangeLicensePlateBikeCommandHandler(ILogger<ChangeLicensePlateBikeCommandHandler> logger,
                                                  IBikesRepository repository) : IRequestHandler<ChangeLicensePlateBikeCommand, Bike?>
{
    public async Task<Bike?> Handle(ChangeLicensePlateBikeCommand request, CancellationToken cancellationToken)
    {
        // busca os dados da moto no banco de dados
        var bikeToChangeLicensePlate = await repository.GetByIdAsync(request.Id);

        if (bikeToChangeLicensePlate is null)
        {
            logger.LogInformation("Não foi encontrada a moto com o Id: {@Id}", request.Id);
            return null;
        }

        // verifica se placa digitada é diferente da atual
        if (bikeToChangeLicensePlate.LicensePlate == request.LicensePlate)
        {
            logger.LogInformation("A nova placa é igual a atual: {@LicensePlate}", request.LicensePlate);
            throw new ConstraintException($"A nova placa é igual a atual: {request.LicensePlate}");
        }

        // verifica se tem outra moto com essa placa no sistema
        var hasAnyBikeWithLicensePlate = await repository.HasAnyBikeWithLicensePlate(request.LicensePlate);

        if (hasAnyBikeWithLicensePlate)
        {
            logger.LogInformation("Já existe uma moto com a placa: {@LicensePlate}", request.LicensePlate);
            throw new ConstraintException($"Já existe uma moto com a placa: {request.LicensePlate}");
        }

        bikeToChangeLicensePlate!.SetLicensePlate(request.LicensePlate);

        logger.LogInformation("Alterando a placa da moto: {@bike}", request);

        _ = await repository.UpdateLicensePlateAsync(bikeToChangeLicensePlate);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Bike(bikeToChangeLicensePlate.EntityId,
                        bikeToChangeLicensePlate.Id,
                        bikeToChangeLicensePlate.Year,
                        bikeToChangeLicensePlate.Model,
                        bikeToChangeLicensePlate.LicensePlate);
    }
}