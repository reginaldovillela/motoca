using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Rentals.Repositories;

public class RentalsRepository(RentalsContext context) : IRentalsRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<RentalEntity> AddAsync(RentalEntity rental)
    {
        _ = await context.AddAsync(rental);

        return rental;
    }

    public async Task<RentalEntity> EndRentalAsync(RentalEntity rental)
    {
        return await Task.Run(() =>
        {
            context.Update(rental);

            return rental;

        });
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
}
