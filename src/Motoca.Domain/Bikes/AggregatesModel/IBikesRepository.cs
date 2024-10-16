using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Bikes.AggregatesModel;

public interface IBikesRepository : IRepository<BikeEntity>
{
    Task<BikeEntity> AddAsync(BikeEntity bike);

    Task<bool> DeleteAsync(BikeEntity bike);

    Task<BikeEntity> GetBikeByIdAsync(string bikeId);

    Task<BikeEntity[]> GetBikesAsync(string? licensePlate);

    Task<bool> HasAnyBikeWithLicensePlate(string licensePlate);

    Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike);
}
