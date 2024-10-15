using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Bikes.AggregatesModel;

public interface IBikesRepository : IRepository<BikeEntity>
{
    Task<BikeEntity> GetBikeByIdAsync(string bikeId);

    Task<BikeEntity[]> GetBikesAsync(string? licensePlate);

    Task<BikeEntity> AddAsync(BikeEntity bike);

    Task<bool> DeleteAsync(BikeEntity bike);

    Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike);

    Task<bool> HasAnyBikeWithLicensePlate(string licensePlate);
}
