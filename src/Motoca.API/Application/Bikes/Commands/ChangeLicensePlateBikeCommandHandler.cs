using Motoca.API.Application.Bikes.Models;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class ChangeLicensePlateBikeCommandHandler : IRequestHandler<ChangeLicensePlateBikeCommand, Bike>
{
    public async Task<Bike> Handle(ChangeLicensePlateBikeCommand request, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            return new Bike("", 0, "", "");
        });
    }
}