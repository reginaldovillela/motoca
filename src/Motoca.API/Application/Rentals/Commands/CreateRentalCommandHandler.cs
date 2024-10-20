using Motoca.API.Application.Rentals.Models;
using Motoca.Domain.Rentals.AggregatesModel;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class CreateRentalCommandHandler(ILogger<CreateRentalCommandHandler> logger,
                                        IRentalsRepository rentalsRepository,
                                        IPlansRepository plansRepository) : IRequestHandler<CreateRentalCommand, Rental?>
{
    public async Task<Rental?> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var plan = await plansRepository.GetByIdAsync(request.PlanId);

        if (plan is null)
        {
            logger.LogInformation("O plano {@Name} não foi encontrado", request.BikeId);
            return null;
        }

        //Todo - Verificar se entregador existe

        //Todo - Verificar se a moto existe

        var newRental = new RentalEntity(request.RiderId, request.BikeId, plan);

        _ = await rentalsRepository.AddAsync(newRental);

        _ = await rentalsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Rental(newRental.EntityId,
                          newRental.Id,
                          newRental.RiderId,
                          newRental.BikeId,
                          new Plan(newRental.Plan.EntityId,
                                   newRental.Plan.Id,
                                   newRental.Plan.DefaultDuration,
                                   newRental.Plan.ValuePerDay),
                          newRental.CreateAt,
                          newRental.StartDate,
                          newRental.ExpectedEndDate,
                          newRental.ReturnDate);
    }
}
