using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Riders.AggregatesModel;

public interface IRidersRepository : IRepository<RiderEntity>
{
    Task<RiderEntity> AddAsync(RiderEntity rider);

    Task<RiderEntity[]> GetAllAsync();

    Task<RiderEntity?> GetByEntityIdAsync(Guid entityId);

    Task<RiderEntity?> GetByIdAsync(string riderId);

    Task<bool> HasAnyRiderWithId(string riderId);
}
