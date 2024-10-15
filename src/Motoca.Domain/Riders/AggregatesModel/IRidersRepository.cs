using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Riders.AggregatesModel;

public interface IRidersRepository : IRepository<RidersEntity>
{
    Task<RidersEntity> AddAsync(RidersEntity rider);

    Task<RidersEntity> GetRiderByIdAsync(string riderId);
}
