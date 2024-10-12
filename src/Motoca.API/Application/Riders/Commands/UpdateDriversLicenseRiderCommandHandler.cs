using Motoca.API.Application.Riders.Models;

namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class UpdateDriversLicenseRiderCommandHandler : IRequestHandler<UpdateDriversLicenseRiderCommand, Rider>
{
    public async Task<Rider> Handle(UpdateDriversLicenseRiderCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Rider());
    }
}
