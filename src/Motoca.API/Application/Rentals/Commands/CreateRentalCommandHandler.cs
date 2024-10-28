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
                                        IRequestClient<GetRiderByIdRequest> riderConsumer) : IRequestHandler<CreateRentalCommand, Rental>
{
    public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var plan = await plansRepository.GetByIdAsync(request.PlanId);

        if (plan is null)
        {
            logger.LogInformation("O plano {@PlanId} não foi encontrado", request.PlanId);
            throw new InvalidOperationException($"O plano {request.PlanId} não foi encontrado");
        }

        // Verificar se a moto existe
        var getBikeRequest = new GetBikeByIdRequest { BikeId = request.BikeId };
        var getBikeResponse = await bikeConsumer.GetResponse<GetBikeByIdResponse>(getBikeRequest, cancellationToken);

        var messageBike = getBikeResponse.Message;

        if (!string.IsNullOrEmpty(messageBike.ErrorMessage))
        {
            logger.LogInformation("A moto {@BikeId} não foi encontrada", request.BikeId);
            throw new InvalidOperationException($"A moto {request.BikeId} não foi encontrada");
        }

        // Verifica se a moto já está alugada
        var bikeHasAlreadyRentaled =  await rentalsRepository.BikeHasAlreadyRentaled(messageBike.Bike.Id);

        if (bikeHasAlreadyRentaled is not null)
        {
            logger.LogInformation("A moto {@BikeId} já está alugada", request.BikeId);
            throw new InvalidOperationException($"A moto {request.BikeId}  já está alugada");
        }

        // Verificar se o entregador existe
        var getRiderRequest = new GetRiderByIdRequest { RiderId = request.RiderId };
        var getRiderResponse = await riderConsumer.GetResponse<GetRiderByIdResponse>(getRiderRequest, cancellationToken);

        var messageRider = getRiderResponse.Message;

        if (!string.IsNullOrEmpty(messageRider.ErrorMessage))
        {
            logger.LogInformation("O entregador {@RiderId} não foi encontrado", request.RiderId);
            throw new InvalidOperationException($"O entregador {request.RiderId} não foi encontrado");
        }

         // Verifica se o entregador já tem um aluguél ativos
        var riderHasAActiveRental =  await rentalsRepository.RiderHasAActiveRental(messageRider.Rider.Id);

        if (riderHasAActiveRental is not null)
        {
            logger.LogInformation("O entregador {@RiderId} já possui o aluguel ativo", request.RiderId);
            throw new InvalidOperationException($"O entregador {request.RiderId} já possui o aluguel ativo");
        }

        // Verifica se o entregador é da Categoria A
        if (messageRider.Rider.DriversLicense.DriversLicenseCategory != "A")
        {
            logger.LogInformation("O entregador {@RiderId} não possui a categoria de CNH adequada", request.RiderId);
            throw new InvalidOperationException($"O entregador {request.RiderId} não possui a categoria de CNH adequada");
        }

        var newRental = new RentalEntity(request.RiderId, request.BikeId, plan);

        _ = await rentalsRepository.AddAsync(newRental);

        _ = await rentalsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Rental(newRental.EntityId,
                          newRental.Id,
                          newRental.RiderId,
                          newRental.BikeId,
                          new Plan(newRental.Plan.EntityId,
                                   newRental.Plan.Id,
                                   newRental.Plan.DurationTime,
                                   newRental.Plan.ValuePerDay),
                          newRental.CreateAt,
                          newRental.StartDate,
                          newRental.ExpectedEndDate,
                          newRental.ReturnDate,
                          newRental.AmountToPay,
                          newRental.IsActive);
    }
}
