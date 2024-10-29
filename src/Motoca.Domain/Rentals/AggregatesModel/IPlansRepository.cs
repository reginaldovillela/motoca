using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IPlansRepository : IRepository<PlanEntity>
{
    Task<ICollection<PlanEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<PlanEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken);

    Task<PlanEntity?> GetByIdAsync(string planId, CancellationToken cancellationToken);
}
