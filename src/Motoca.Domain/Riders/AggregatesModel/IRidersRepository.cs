using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Riders.AggregatesModel;

public interface IRidersRepository : IRepository<RiderEntity>
{
    Task<RiderEntity> AddAsync(RiderEntity rider);

    Task<RiderEntity> GetRiderByIdAsync(string riderId);
}
