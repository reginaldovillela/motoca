using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Bikes.Repositories;

public class BikesRepository(BikesContext context) : IBikesRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<BikeEntity> AddAsync(BikeEntity bike)
    {
        await context.Bikes.AddAsync(bike);

        return bike;
    }

    public async Task<bool> DeleteAsync(BikeEntity bike)
    {
        return await Task.Run(() =>
        {
            _ = context.Bikes.Remove(bike);

            return true;
        });
    }

    public async Task<BikeEntity[]> GetAllAsync(string? licensePlate)
    {
        return await Task.Run(() =>
        {
            var bikes = context.Bikes.AsNoTracking();

            if (licensePlate is not null)
                bikes = bikes.Where(x => x.LicensePlate == licensePlate);

            return bikes.ToArray();
        });
    }

    public async Task<BikeEntity> GetByIdAsync(string bikeId)
    {
        var bike = await context.Bikes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == bikeId);

        return bike;
    }

    public async Task<BikeEntity> GetByInternalIdAsync(Guid entityId)
    {
        var bike = await context.Bikes.AsNoTracking().SingleOrDefaultAsync(x => x.EntityId == entityId);

        return bike;
    }

    public async Task<bool> HasAnyBikeWithId(string id)
    {
        var bikes = await context.Bikes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return bikes is not null;
    }

    public async Task<bool> HasAnyBikeWithLicensePlate(string licensePlate)
    {
        var bikes = await context.Bikes.AsNoTracking().FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);

        return bikes is not null;
    }

    public async Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike)
    {
        return await Task.Run(() =>
        {
            _ = context.Bikes.Update(bike);

            return bike;
        });
    }
}
