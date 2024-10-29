using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Riders.AggregatesModel;

public interface IRidersRepository : IRepository<RiderEntity>
{
    Task<RiderEntity> AddAsync(RiderEntity rider, CancellationToken cancellationToken);

    Task<ICollection<RiderEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<RiderEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken);

    Task<RiderEntity?> GetByIdAsync(string riderId, CancellationToken cancellationToken);

    Task<bool> HasAnyRiderWithId(string riderId, CancellationToken cancellationToken);
}
