using System.Data;
using Motoca.API.Application.Riders.Models;
using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class UpdateDriversLicenseRiderCommandHandler(ILogger<UpdateDriversLicenseRiderCommandHandler> logger,
                                                     IRidersRepository repository,
                                                     IRidersStorageService storageService) : IRequestHandler<UpdateDriversLicenseRiderCommand, Rider?>
{
    public async Task<Rider?> Handle(UpdateDriversLicenseRiderCommand request, CancellationToken cancellationToken)
    {
        var riderToUpdateDriversLicense = await repository.GetByIdAsync(request.Id);

        if (riderToUpdateDriversLicense is null)
        {
            logger.LogInformation("Entregador com o Id {@Id} não foi encontrado", request.Id);
            throw new ConstraintException($"Entregador com o Id {request.Id} não foi encontrado");
        }

        var driversLicenseImageBytes = Convert.FromBase64String(request.DriversLicenseImage);

        await storageService.SaveFileAsync(riderToUpdateDriversLicense.DriversLicense.EntityId.ToString(), driversLicenseImageBytes);

        return new Rider(riderToUpdateDriversLicense.EntityId,
                         riderToUpdateDriversLicense.Id,
                         riderToUpdateDriversLicense.Name,
                         riderToUpdateDriversLicense.SocialId.Number,
                         riderToUpdateDriversLicense.BirthDate,
                         new DriversLicense(riderToUpdateDriversLicense.DriversLicense.EntityId,
                                            riderToUpdateDriversLicense.DriversLicense.Number,
                                            riderToUpdateDriversLicense.DriversLicense.Category,
                                            request.DriversLicenseImage));
    }
}
