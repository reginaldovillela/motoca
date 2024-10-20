using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Rentals.Queries;

#pragma warning disable 1591
public class GetPlansQueryHandler(ILogger<GetPlansQueryHandler> logger,
                                  IPlansRepository repository) : IRequestHandler<GetPlansQuery, Plan[]>
{
    public async Task<Plan[]> Handle(GetPlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await repository.GetAllAsync();

        logger.LogInformation("Consulta concluída. Total de {@count} encontrados", plans.Length);

        return plans.Select(p => new Plan(p.EntityId,
                                          p.Id,
                                          p.DefaultDuration,
                                          p.ValuePerDay))
                    .ToArray();
    }
}
