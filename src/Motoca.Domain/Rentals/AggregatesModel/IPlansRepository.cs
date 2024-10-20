using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IPlansRepository : IRepository<PlansEntity>
{
    Task<PlansEntity[]> GetAllAsync();

    Task<PlansEntity?> GetByEntityIdAsync(Guid entityId);

    Task<PlansEntity?> GetByIdAsync(string planId);
}
