using Motoca.API.Application.Rentals.Models;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class EndRentalCommandHandler : IRequestHandler<EndRentalCommand, Rental>
{
    public async Task<Rental> Handle(EndRentalCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Rental(new Guid(), 10, "", "", DateTime.Now, DateTime.Now, DateTime.Now, null));
    }
}
