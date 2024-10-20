using System.Data;
using Motoca.API.Application.Riders.Models;
using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class CreateRiderCommandHandler(ILogger<CreateRiderCommandHandler> logger,
                                       IRidersRepository repository,
                                       IRidersStorageService storageService) : IRequestHandler<CreateRiderCommand, Rider>
{
    public async Task<Rider> Handle(CreateRiderCommand request, CancellationToken cancellationToken)
    {
        var hasAnyRiderWithId = await repository.HasAnyRiderWithId(request.Id);

        if (hasAnyRiderWithId)
        {
            logger.LogInformation("Já existe um entregador com o Id: {@Id}", request.Id);
            throw new ConstraintException($"Já existe um entregador com o Id: {request.Id}");
        }

        var newRider = new RiderEntity(request.Id,
                                       request.Name,
                                       request.SocialId,
                                       request.BirthDate);
        newRider.SetDriversLicense(request.DriversLicenseNumber,
                                   request.DriversLicenseCategory);

        var driversLicenseImageBytes = Convert.FromBase64String(request.DriversLicenseImage);

        _ = await repository.AddAsync(newRider);

        _ = await storageService.SaveFileAsync(newRider.DriversLicense.EntityId.ToString(),
                                               driversLicenseImageBytes);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Salvando no entregador @{rider}", newRider);

        return new Rider(newRider.EntityId,
                         newRider.Id,
                         newRider.Name,
                         newRider.SocialId.Number,
                         newRider.BirthDate,
                         new DriversLicense(newRider.DriversLicense.EntityId,
                                            newRider.DriversLicense.Number,
                                            newRider.DriversLicense.Category,
                                            request.DriversLicenseImage));
    }
}
