using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Rentals.Repositories;

public class PlansRepository : IPlansRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<PlansEntity> GetPlanByIdAsync(string planId)
    {
        throw new NotImplementedException();
    }

    public Task<PlansEntity> GetPlanByNameAsync(string planName)
    {
        throw new NotImplementedException();
    }

    public Task<PlansEntity[]> GetPlansAsync()
    {
        throw new NotImplementedException();
    }
}
