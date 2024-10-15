using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IPlansRepository : IRepository<PlansEntity>
{
    Task<PlansEntity> GetPlanByIdAsync(string planId);

    Task<PlansEntity> GetPlanByNameAsync(string planName);
    
    Task<PlansEntity[]> GetPlansAsync();
}
