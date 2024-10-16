using Motoca.API.Application.Riders.Models;
using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class CreateRiderCommandHandler(ILogger<CreateRiderCommandHandler> logger,
                                       IRidersRepository repository) : IRequestHandler<CreateRiderCommand, Rider>
{
    public async Task<Rider> Handle(CreateRiderCommand request, CancellationToken cancellationToken)
    {
        var newRider = new RiderEntity(request.Id, request.Name, request.SocialId);

        _ = await repository.AddAsync(newRider);

        logger.LogInformation("Salvando no entregador @{rider}", newRider);

        _ = await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Rider(newRider.Id,
                         newRider.Name,
                         newRider.CPF.Number,
                         newRider.BirthDate,
                         new DriversLicense(newRider.DriversLicense.Number,
                                            newRider.DriversLicense.Category,
                                            newRider.DriversLicense.Base64Image));
    }
}
