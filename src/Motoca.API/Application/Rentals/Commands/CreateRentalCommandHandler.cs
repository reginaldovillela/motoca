using System.Data;
using Motoca.API.Application.Rentals.Models;
using Motoca.Domain.Rentals.AggregatesModel;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class CreateRentalCommandHandler(ILogger<CreateRentalCommandHandler> logger,
                                        IRentalsRepository rentalsRepository,
                                        IPlansRepository plansRepository) : IRequestHandler<CreateRentalCommand, Rental>
{
    public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var plan = await plansRepository.GetPlanByNameAsync("");

        if (plan is null)
        {
             logger.LogInformation("O plano {@Name} não foi encontrado", request.BikeId);
            throw new ConstraintException($"O plano {request.BikeId} não foi encontrado");
        }

        //Todo - Verificar se entregador existe

        //Todo - Verificar se a moto existe

        var newRental = new RentalEntity();


        _ = await rentalsRepository.AddAsync(newRental);

        _ = await rentalsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Rental(newRental.EntityId, 10, "", "", DateTime.Now, DateTime.Now, DateTime.Now, null);
    }
}
