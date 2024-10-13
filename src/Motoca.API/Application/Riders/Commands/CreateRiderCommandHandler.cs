using Motoca.API.Application.Riders.Models;

namespace Motoca.API.Application.Riders.Commands;

#pragma warning disable 1591
public class CreateRiderCommandHandler : IRequestHandler<CreateRiderCommand, Rider>
{
    public async Task<Rider> Handle(CreateRiderCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Rider("", "", "", DateOnly.MinValue, new DriversLicense("", "", "")));
    }
}
