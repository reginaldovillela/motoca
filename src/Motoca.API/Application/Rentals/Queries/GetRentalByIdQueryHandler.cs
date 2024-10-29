using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Rentals.Queries;

#pragma warning disable 1591
public class GetRentalByIdQueryHandler(ILogger<GetRentalByIdQueryHandler> logger,
                                       IRentalsRepository repository) : IRequestHandler<GetRentalByIdQuery, Rental?>
{
    public async Task<Rental?> Handle(GetRentalByIdQuery request, CancellationToken cancellationToken)
    {
        var rental = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (rental is null)
        {
            logger.LogInformation("Não foi encontrada a locação com o Id: {@Id}", request.Id);
            return null;
        }

        rental.Recalculate();

        return new Rental(rental.EntityId, 
                          rental.Id, 
                          rental.RiderId, 
                          rental.BikeId, 
                          new Plan(rental.Plan.EntityId, 
                                   rental.Plan.Id, 
                                   rental.Plan.DurationTime, 
                                   rental.Plan.ValuePerDay,
                                   rental.Plan.PenaltyPercent), 
                          rental.CreateAt, 
                          rental.StartDate, 
                          rental.ExpectedEndDate, 
                          rental.ReturnDate,
                          rental.AmountToPay,
                          rental.IsActive,
                          rental.IsOverDue,
                          rental.DaysInRental,
                          rental.DaysOverDue);
    }
}
