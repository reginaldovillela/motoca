namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class EndRentalCommandHandler : IRequestHandler<EndRentalCommand, string>
{
    public async Task<string> Handle(EndRentalCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult("");
    }
}
