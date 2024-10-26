using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Bikes.AggregatesModel;

public interface IBikesRepository : IRepository<BikeEntity>
{
    Task<BikeEntity> AddAsync(BikeEntity bike, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(BikeEntity bike, CancellationToken cancellationToken);

    Task<BikeEntity[]> GetAllAsync(string? licensePlate, CancellationToken cancellationToken);

    Task<BikeEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken);

    Task<BikeEntity?> GetByIdAsync(string bikeId, CancellationToken cancellationToken);

    Task<bool> HasAnyBikeWithId(string bikeId, CancellationToken cancellationToken);

    Task<bool> HasAnyBikeWithLicensePlate(string licensePlate, CancellationToken cancellationToken);

    Task<BikeEntity> UpdateLicensePlateAsync(BikeEntity bike, CancellationToken cancellationToken);
}
