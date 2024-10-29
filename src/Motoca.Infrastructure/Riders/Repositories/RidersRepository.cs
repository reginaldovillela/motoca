using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Riders.Repositories;

public class RidersRepository(RidersContext context) : IRidersRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<RiderEntity> AddAsync(RiderEntity rider, CancellationToken cancellationToken)
    {
        _ = await context.Riders.AddAsync(rider, cancellationToken);

        return rider;
    }

    public async Task<ICollection<RiderEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var riders = await context
                         .Riders
                         .Include(r => r.DriversLicense)
                         .AsNoTracking()
                         .ToListAsync(cancellationToken);

        return riders;
    }

    public async Task<RiderEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var rider = await context
                            .Riders
                            .Include(r => r.DriversLicense)
                            .Where(r => r.EntityId == entityId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return rider;
    }

    public async Task<RiderEntity?> GetByIdAsync(string riderId, CancellationToken cancellationToken)
    {
        var rider = await context
                            .Riders
                            .Include(r => r.DriversLicense)
                            .Where(r => r.Id == riderId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return rider;
    }

    public async Task<bool> HasAnyRiderWithId(string riderId, CancellationToken cancellationToken)
    {
        var rider = await context
                            .Riders
                            .Where(r => r.Id == riderId)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(cancellationToken);

        return rider is not null;
    }
}
