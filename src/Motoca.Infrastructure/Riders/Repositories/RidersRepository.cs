
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

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
           var riders = context
                            .Riders
                            .Include(r => r.DriversLicense)
                            .AsNoTracking();

           return riders.ToArray();
       });
    }

    public async Task<RiderEntity?> GetByEntityIdAsync(Guid entityId)
    {
        var rider = await context
                            .Riders
                            .Include(r => r.DriversLicense)
                            .Where(r => r.EntityId == entityId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();

        return rider;
    }

    public async Task<RiderEntity?> GetByIdAsync(string riderId)
    {
        var rider = await context
                            .Riders
                            .Include(r => r.DriversLicense)
                            .Where(r => r.Id == riderId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();

        return rider;
    }

    public async Task<bool> HasAnyRiderWithId(string riderId)
    {
        var rider = await context
                            .Riders
                            .Where(r => r.Id == riderId)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

        return rider is not null;
    }
}
