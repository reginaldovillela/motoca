using Motoca.API.Application.Rentals.Models;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental>
{
    public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Rental(new Guid()));
    }
}
