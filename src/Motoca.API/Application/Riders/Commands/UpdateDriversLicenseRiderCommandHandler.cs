using System.Data;
using Motoca.API.Application.Riders.Models;
using Motoca.Domain.Riders.AggregatesModel;

namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class UpdateDriversLicenseRiderCommandHandler(ILogger<UpdateDriversLicenseRiderCommandHandler> logger,
                                                     IRidersRepository repository) : IRequestHandler<UpdateDriversLicenseRiderCommand, Rider>
{
    public async Task<Rider> Handle(UpdateDriversLicenseRiderCommand request, CancellationToken cancellationToken)
    {
        var rider = await repository.GetRiderByIdAsync(request.Id);

        if (rider is null)
        {
            logger.LogInformation("Entregador com o Id {@Id} não foi encontrado", request.Id);
            throw new ConstraintException($"Entregador com o Id {request.Id} não foi encontrado");
        }

        //Todo alterar a foto

        return new Rider(rider.Id, 
                         rider.Name, 
                         rider.CPF.Number, 
                         rider.BirthDate, 
                         new DriversLicense(rider.DriversLicense.Number, 
                                            rider.DriversLicense.Category, 
                                            rider.DriversLicense.Base64Image));
    }
}
