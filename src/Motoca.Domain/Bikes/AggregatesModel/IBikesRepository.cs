using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Bikes.AggregatesModel;

public interface IBikesRepository : IRepository<BikeEntity>
{
    Task<BikeEntity> AddAsync(BikeEntity bike);

    Task<bool> DeleteAsync(BikeEntity bike);

    Task<BikeEntity[]> GetAllAsync(string? licensePlate);

    Task<BikeEntity?> GetByEntityIdAsync(Guid entityId);

    Task<BikeEntity?> GetByIdAsync(string bikeId);

    Task<bool> HasAnyBikeWithId(string bikeId);

    Task<bool> HasAnyBikeWithLicensePlate(string licensePlate);

    Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike);
}
