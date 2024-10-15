
using Motoca.Domain.Riders.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Riders.Repositories;

public class RidersRepository : IRidersRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<RiderEntity> AddAsync(RiderEntity rider)
    {
        throw new NotImplementedException();
    }

    public Task<RiderEntity> GetRiderByIdAsync(string riderId)
    {
        throw new NotImplementedException();
    }
}
