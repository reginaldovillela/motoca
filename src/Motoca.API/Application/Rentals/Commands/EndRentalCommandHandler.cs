using Motoca.API.Application.Rentals.Models;
using Motoca.Domain.Rentals.AggregatesModel;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class EndRentalCommandHandler(ILogger<EndRentalCommandHandler> logger,
                                     IRentalsRepository rentalsRepository,
                                     IPlansRepository plansRepository) : IRequestHandler<EndRentalCommand, Rental>
{
    public async Task<Rental> Handle(EndRentalCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Rental(new Guid(), 10, "", "", DateTime.Now, DateTime.Now, DateTime.Now, null));
    }
}
