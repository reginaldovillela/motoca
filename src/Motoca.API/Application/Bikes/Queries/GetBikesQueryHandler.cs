using Motoca.API.Application.Bikes.Models;

namespace Motoca.API.Application.Bikes.Queries;

#pragma warning disable 1591
public class GetBikesQueryHandler : IRequestHandler<GetBikesQuery, Bike[]>
{
    public async Task<Bike[]> Handle(GetBikesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Bike[1]);
    }
}
