using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Bikes.AggregatesModel;

public interface IBikesRepository : IRepository<BikeEntity>
{
    Task<BikeEntity> AddAsync(BikeEntity bike);

    Task<bool> DeleteAsync(BikeEntity bike);

    Task<BikeEntity[]> GetAllAsync(string? licensePlate);

    Task<BikeEntity> GetByIdAsync(string bikeId);

    Task<BikeEntity> GetByInternalIdAsync(Guid entityId);

    Task<bool> HasAnyBikeWithId(string id);

    Task<bool> HasAnyBikeWithLicensePlate(string licensePlate);

    Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike);
}
