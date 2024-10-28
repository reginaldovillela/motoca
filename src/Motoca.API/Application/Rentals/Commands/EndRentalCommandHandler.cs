using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class EndRentalCommandHandler(ILogger<EndRentalCommandHandler> logger,
                                     IRentalsRepository repository) : IRequestHandler<EndRentalCommand, Rental?>
{
    public async Task<Rental?> Handle(EndRentalCommand request, CancellationToken cancellationToken)
    {
        var rentalToEnd = await repository.GetByIdAsync(request.Id);

        if (rentalToEnd is null)
        {
            logger.LogInformation("A locação com o Id {@Id} não foi encontrada", request.Id);
            return null;
        }

        //Todo Calcular devolução

        _ = await repository.EndRentalAsync(rentalToEnd);

        _ = await repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        logger.LogInformation("Finalizando a locação: {@rental}", request);

        return new Rental(rentalToEnd.EntityId, 
                          rentalToEnd.Id, 
                          rentalToEnd.RiderId, 
                          rentalToEnd.BikeId, 
                          new Plan(rentalToEnd.Plan.EntityId, 
                                   rentalToEnd.Plan.Id, 
                                   rentalToEnd.Plan.DurationTime, 
                                   rentalToEnd.Plan.ValuePerDay), 
                          rentalToEnd.CreateAt, 
                          rentalToEnd.StartDate, 
                          rentalToEnd.ExpectedEndDate, 
                          rentalToEnd.ReturnDate,
                          rentalToEnd.AmountToPay,
                          rentalToEnd.IsActive);
    }
}
