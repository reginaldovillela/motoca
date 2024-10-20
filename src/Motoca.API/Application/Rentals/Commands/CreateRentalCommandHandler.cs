using MassTransit;
using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Application.Models;
using Motoca.SharedKernel.Message;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class CreateRentalCommandHandler(ILogger<CreateRentalCommandHandler> logger,
                                        IRentalsRepository rentalsRepository,
                                        IPlansRepository plansRepository,
                                        IRequestClient<GetBikeByIdRequest> bikeConsumer,
                                        IRequestClient<GetRiderByIdRequest> riderConsumer) : IRequestHandler<CreateRentalCommand, Rental?>
{
    public async Task<Rental?> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var plan = await plansRepository.GetByIdAsync(request.PlanId);

        if (plan is null)
        {
            logger.LogInformation("O plano {@Name} não foi encontrado", request.BikeId);
            return null;
        }

        //Todo - Verificar se a moto existe
        var getBikeRequest = new GetBikeByIdRequest { BikeId = request.BikeId };
        var getBikeResponse = await bikeConsumer.GetResponse<GetBikeByIdResponse>(getBikeRequest, cancellationToken);

        //Todo - Verificar se entregador existe
        var getRiderRequest = new GetRiderByIdRequest { RiderId = request.RiderId };
        var getRiderResponse = await riderConsumer.GetResponse<GetRiderByIdResponse>(getRiderRequest, cancellationToken);

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
