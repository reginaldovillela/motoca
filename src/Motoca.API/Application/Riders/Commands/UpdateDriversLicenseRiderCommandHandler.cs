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

        return await Task.FromResult(new Rider("", "", "", DateOnly.MinValue, new DriversLicense("", "", "")));
    }
}
