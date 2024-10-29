using Motoca.Domain.Riders.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Riders.Queries;

#pragma warning disable 1591
public class GetRiderByIdQueryHandler(ILogger<GetRiderByIdQueryHandler> logger,
                                      IRidersRepository repository,
                                      IRidersStorageService storageService) : IRequestHandler<GetRiderByIdQuery, Rider?>
{
    public async Task<Rider?> Handle(GetRiderByIdQuery request, CancellationToken cancellationToken)
    {
        var rider = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (rider is null)
        {
            logger.LogInformation("Não foi encontrado o entregador com o Id: {@Id}", request.Id);
            return null;
        }

        var driversLicenseImage = await storageService.GetFileAsync(rider.DriversLicense.EntityId.ToString());

        return new Rider(rider.EntityId,
                         rider.Id,
                         rider.Name,
                         rider.SocialId.Number,
                         rider.BirthDate,
                         new DriversLicense(rider.DriversLicense.EntityId,
                                            rider.DriversLicense.Number,
                                            rider.DriversLicense.Category,
                                            Convert.ToBase64String(driversLicenseImage)));
    }
}
