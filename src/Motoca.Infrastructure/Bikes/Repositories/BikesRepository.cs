using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Bikes.Repositories;

public class BikesRepository(BikesContext context) : IBikesRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<BikeEntity> AddAsync(BikeEntity bike, CancellationToken cancellationToken)
    {
        _ = await context.Bikes.AddAsync(bike, cancellationToken);

        return bike;
    }

    public async Task<bool> DeleteAsync(BikeEntity bike, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            _ = context.Bikes.Remove(bike);

            return true;
        }, cancellationToken);
    }

    public async Task<BikeEntity[]> GetAllAsync(string? licensePlate, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            var bikes = context.Bikes.AsNoTracking();

            if (licensePlate is not null)
                bikes = bikes.Where(x => x.LicensePlate == licensePlate);

            return bikes.ToArray();
        }, cancellationToken);
    }

    public async Task<BikeEntity?> GetByIdAsync(string bikeId, CancellationToken cancellationToken)
    {
        var bike = await context
                            .Bikes
                            .Where(b => b.Id == bikeId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return bike;
    }

    public async Task<BikeEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var bike = await context
                            .Bikes
                            .Where(b => b.EntityId == entityId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return bike;
    }

    public async Task<bool> HasAnyBikeWithId(string bikeId, CancellationToken cancellationToken)
    {
        var bikes = await context
                            .Bikes
                            .Where(b => b.Id == bikeId)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(cancellationToken);

        return bikes is not null;
    }

    public async Task<bool> HasAnyBikeWithLicensePlate(string licensePlate, CancellationToken cancellationToken)
    {
        var bikes = await context
                            .Bikes
                            .Where(b => b.LicensePlate == licensePlate)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(cancellationToken);

        return bikes is not null;
    }

    public async Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            _ = context.Bikes.Update(bike);

            return bike;
        }, cancellationToken);
    }
}
