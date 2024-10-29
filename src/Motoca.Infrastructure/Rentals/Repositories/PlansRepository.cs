using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Rentals.Repositories;

public class PlansRepository(RentalsContext context) : IPlansRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<ICollection<PlanEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var plans = await context
                            .Plans
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

        return plans;
    }

    public async Task<PlanEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var plan = await context
                            .Plans
                            .Where(p => p.EntityId == entityId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return plan;
    }

    public async Task<PlanEntity?> GetByIdAsync(string planId, CancellationToken cancellationToken)
    {
        var plan = await context
                            .Plans
                            .Where(p => p.Id == planId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(cancellationToken);

        return plan;
    }
}
