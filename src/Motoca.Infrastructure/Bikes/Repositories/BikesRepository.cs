using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Bikes.Repositories;

public class BikesRepository : IBikesRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<BikeEntity> AddAsync(BikeEntity bike)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(BikeEntity bike)
    {
        throw new NotImplementedException();
    }

    public Task<BikeEntity> GetBikeByIdAsync(string bikeId)
    {
        throw new NotImplementedException();
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
