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
        _ = await Task.FromResult(context.Bikes.Remove(bike));

        return true;
    }

    public async Task<BikeEntity> GetBikeByIdAsync(string bikeId)
    {
        var bike = await context.Bikes.SingleAsync(x => x.Id == bikeId);

        return bike;
    }

    public Task<BikeEntity[]> GetBikesAsync(string? licensePlate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasAnyBikeWithLicensePlate(string licensePlate)
    {
        throw new NotImplementedException();
    }

    public Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike)
    {
        throw new NotImplementedException();
    }
}
