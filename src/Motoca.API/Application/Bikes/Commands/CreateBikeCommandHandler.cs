using Motoca.API.Application.Bikes.Models;

namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public class CreateBikeCommandHandler : IRequestHandler<CreateBikeCommand, Bike>
{
    public async Task<Bike> Handle(CreateBikeCommand request, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            return new Bike("", 0, "", "");
        });
    }
}