using Motoca.API.Application.Bikes.Models;

namespace Motoca.API.Application.Bikes.Queries;

#pragma warning disable 1591
public class GetBikeByIdQueryHandler : IRequestHandler<GetBikeByIdQuery, Bike>
{
    public async Task<Bike> Handle(GetBikeByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Bike("", 0, "", ""));
    }
}
