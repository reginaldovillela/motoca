using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Rentals.Repositories;

public class RentalsRepository(RentalsContext context)
    : IRentalsRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<RentalEntity> AddAsync(RentalEntity rental, CancellationToken cancellationToken)
    {
        context.Entry(rental.Plan).State = EntityState.Unchanged;

        _ = await context.AddAsync(rental, cancellationToken);

        return rental;
    }

    public async Task<RentalEntity?> BikeHasAlreadyBeenRentals(string bikeId, CancellationToken cancellationToken)
    {
        var rental = await context
                                .Rentals
                                .OrderByDescending(r => r.StartDate)
                                .Where(r => r.BikeId == bikeId)
                                .FirstOrDefaultAsync(cancellationToken);

        return rental;
    }

    public async Task<RentalEntity?> BikeHasAlreadyRentaled(string bikeId, CancellationToken cancellationToken)
    {
        var rental = await context
                                .Rentals
                                .OrderByDescending(r => r.StartDate)
                                .Where(r => r.BikeId == bikeId &&
                                           r.ReturnDate == null)
                                .FirstOrDefaultAsync(cancellationToken);

        return rental;
    }

    public async Task<RentalEntity> EndRentalAsync(RentalEntity rental, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            context.Entry(rental.Plan).State = EntityState.Unchanged;

            context.Update(rental);

            return rental;
        }, cancellationToken);
    }

    public async Task<ICollection<RentalEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var rentals = await context
                                .Rentals
                                .Include(r => r.Plan)
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

        return rentals;

    }

    public async Task<RentalEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var rental = await context
                            .Rentals
                            .Include(r => r.Plan)
                            .Where(p => p.EntityId == entityId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return rental;
    }

    public async Task<RentalEntity?> GetByIdAsync(string rentalId, CancellationToken cancellationToken)
    {
        var rental = await context
                            .Rentals
                            .Include(r => r.Plan)
                            .Where(p => p.Id == rentalId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return rental;
    }

    public async Task<RentalEntity?> RiderHasAActiveRental(string riderId, CancellationToken cancellationToken)
    {
        var rental = await context
                                .Rentals
                                .OrderByDescending(r => r.StartDate)
                                .Where(r => r.RiderId == riderId &&
                                           r.ReturnDate == null)
                                .FirstOrDefaultAsync(cancellationToken);

        return rental;
    }
}
