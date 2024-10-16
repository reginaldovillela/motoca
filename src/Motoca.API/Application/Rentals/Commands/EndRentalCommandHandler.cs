using System.Data;
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
        var rentalToEnd = await rentalsRepository.GetRentalByIdAsync(request.Id);

        if (rentalToEnd is null)
        {
            logger.LogInformation("A locação com o Id {@Id} não foi encontrada", request.Id);
            throw new ConstraintException($"A locação com o Id {request.Id} não foi encontrada");
        }

        //Todo Calcular devolução

        _ = await rentalsRepository.EndRentalAsync(rentalToEnd);

        _ = await rentalsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return await Task.FromResult(new Rental(new Guid(), 10, "", "", DateTime.Now, DateTime.Now, DateTime.Now, null));
    }
}
