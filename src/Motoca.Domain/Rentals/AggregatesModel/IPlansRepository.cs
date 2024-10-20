using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IPlansRepository : IRepository<PlanEntity>
{
    Task<PlanEntity[]> GetAllAsync();

    Task<PlanEntity?> GetByEntityIdAsync(Guid entityId);

    Task<PlanEntity?> GetByIdAsync(string planId);
}
