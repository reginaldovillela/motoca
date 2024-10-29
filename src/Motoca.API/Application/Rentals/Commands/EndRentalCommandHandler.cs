using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Rentals.Commands;

#pragma warning disable 1591
public class EndRentalCommandHandler(ILogger<EndRentalCommandHandler> logger,
                                     IRentalsRepository repository) : IRequestHandler<EndRentalCommand, Rental?>
{
    public async Task<Rental?> Handle(EndRentalCommand request, CancellationToken cancellationToken)
    {
        var rentalToEnd = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (rentalToEnd is null)
        {
            logger.LogInformation("A locação com o Id {@Id} não foi encontrada", request.Id);
            return null;
        }

        if (!rentalToEnd.IsActive)
        {
            logger.LogInformation("A locação com o Id {@Id} já foi encerrada no dia {@ReturnDate}", request.Id, rentalToEnd.ReturnDate);
            throw new InvalidOperationException($"A locação com o Id {request.Id} já foi encerrada no dia {rentalToEnd.ReturnDate}");
        }

        rentalToEnd.EndRental(request.ReturnDate);

        _ = await repository.EndRentalAsync(rentalToEnd, cancellationToken);

        _ = await repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        logger.LogInformation("Finalizando a locação: {@Rental}", request);

        return new Rental(rentalToEnd.EntityId,
                          rentalToEnd.Id,
                          rentalToEnd.RiderId,
                          rentalToEnd.BikeId,
                          new Plan(rentalToEnd.Plan.EntityId,
                                   rentalToEnd.Plan.Id,
                                   rentalToEnd.Plan.DurationTime,
                                   rentalToEnd.Plan.ValuePerDay,
                                   rentalToEnd.Plan.PenaltyPercent),
                          rentalToEnd.CreateAt,
                          rentalToEnd.StartDate,
                          rentalToEnd.ExpectedEndDate,
                          rentalToEnd.ReturnDate,
                          rentalToEnd.AmountToPay,
                          rentalToEnd.IsActive,
                          rentalToEnd.IsOverDue,
                          rentalToEnd.DaysInRental,
                          rentalToEnd.DaysOverDue);
    }
}
