using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Rentals.Repositories;

public class PlansRepository(RentalsContext context) : IPlansRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<ICollection<PlanEntity>> GetAllAsync()
    {
        var plans = await context
                            .Plans
                            .AsNoTracking()
                            .ToListAsync();

        return plans;
    }

    public async Task<PlanEntity?> GetByEntityIdAsync(Guid entityId)
    {
        var plan = await context
                            .Plans
                            .Where(p => p.EntityId == entityId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();

        return plan;
    }

    public async Task<PlanEntity?> GetByIdAsync(string planId)
    {
        var plan = await context
                            .Plans
                            .Where(p => p.Id == planId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();

        return plan;
    }
}
