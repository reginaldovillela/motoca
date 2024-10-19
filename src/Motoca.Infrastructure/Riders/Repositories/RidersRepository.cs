
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;
using Motoca.Infrastructure.Bikes;

namespace Motoca.Infrastructure.Riders.Repositories;

public class RidersRepository(RidersContext context) : IRidersRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<RiderEntity> AddAsync(RiderEntity rider)
    {
        _ = await context.Riders.AddAsync(rider);

        return rider;
    }

    public async Task<RiderEntity[]> GetAllAsync()
    {
        return await Task.Run(() =>
       {
           var riders = context.Riders.Include(r => r.DriversLicense).AsNoTracking();

           return riders.ToArray();
       });
    }

    public async Task<RiderEntity?> GetByEntityIdAsync(Guid entityId)
    {
        var rider = await context.Riders.Include(r => r.DriversLicense).AsNoTracking().SingleOrDefaultAsync(x => x.EntityId == entityId);

        return rider;
    }

    public async Task<RiderEntity?> GetByIdAsync(string riderId)
    {
        var rider = await context.Riders.Include(r => r.DriversLicense).AsNoTracking().SingleOrDefaultAsync(x => x.Id == riderId);

        return rider;
    }

    public async Task<bool> HasAnyRiderWithId(string riderId)
    {
        var rider = await context.Riders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == riderId);

        return rider is not null;
    }
}
