using Motoca.API.Application.Rentals.Models;

namespace Motoca.API.Application.Rentals.Queries;

#pragma warning disable 1591
public class GetRentalByIdQueryHandler : IRequestHandler<GetRentalByIdQuery, Rental>
{
    public async Task<Rental> Handle(GetRentalByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Rental(new Guid(), 10, "", "", DateTime.Now, DateTime.Now, DateTime.Now, null));
    }
}
