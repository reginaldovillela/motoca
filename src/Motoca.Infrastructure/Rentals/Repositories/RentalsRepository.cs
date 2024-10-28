using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Rentals.Repositories;

public class RentalsRepository(RentalsContext context)
    : IRentalsRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<RentalEntity> AddAsync(RentalEntity rental)
    {
        context.Entry(rental.Plan).State = EntityState.Unchanged;

        _ = await context.AddAsync(rental);

        return rental;
    }

    public async Task<RentalEntity?> BikeHasAlreadyRentaled(string bikeId)
    {
        var rental = await context
                                .Rentals
                                .OrderByDescending(r => r.StartDate)
                                .Where(r => r.BikeId == bikeId &&
                                           r.ReturnDate == null)
                                .FirstOrDefaultAsync();

        return rental;
    }

    public async Task<RentalEntity> EndRentalAsync(RentalEntity rental)
    {
        return await Task.Run(() =>
        {
            context.Entry(rental.Plan).State = EntityState.Unchanged;

            context.Update(rental);

            return rental;
        });
    }

    public async Task<ICollection<RentalEntity>> GetAllAsync()
    {
        var rentals = await context
                                .Rentals
                                .Include(r => r.Plan)
                                .AsNoTracking()
                                .ToListAsync();

        return rentals;

    }

    public async Task<RentalEntity?> GetByEntityIdAsync(Guid entityId)
    {
        var rental = await context
                            .Rentals
                            .Include(r => r.Plan)
                            .Where(p => p.EntityId == entityId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();

        return rental;
    }

    public async Task<RentalEntity?> GetByIdAsync(string rentalId)
    {
        var rental = await context
                            .Rentals
                            .Include(r => r.Plan)
                            .Where(p => p.Id == rentalId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();

        return rental;
    }

    public async Task<RentalEntity?> RiderHasAActiveRental(string riderId)
    {
        var rental = await context
                                .Rentals
                                .OrderByDescending(r => r.StartDate)
                                .Where(r => r.RiderId == riderId &&
                                           r.ReturnDate == null)
                                .FirstOrDefaultAsync();

        return rental;
    }
}
