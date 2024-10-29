using Motoca.Domain.Riders.AggregatesModel;
using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Riders.Queries;

#pragma warning disable 1591
public class GetRidersQueryHandler(ILogger<GetRidersQueryHandler> logger,
                                   IRidersRepository repository) : IRequestHandler<GetRidersQuery, Rider[]>
{
    public async Task<Rider[]> Handle(GetRidersQuery request, CancellationToken cancellationToken)
    {
        var riders = await repository.GetAllAsync(cancellationToken);

         logger.LogInformation("Consulta concluída. Total de {@Count} encontrados", riders.Count);

        return riders.Select(r => new Rider(r.EntityId, 
                                            r.Id, 
                                            r.Name, 
                                            r.SocialId.Number, 
                                            r.BirthDate, 
                                            new DriversLicense(r.DriversLicense.EntityId, 
                                                               r.DriversLicense.Number, 
                                                               r.DriversLicense.Category, 
                                                               "Não será exibido aqui. Buscar por ID")))
                     .ToArray();
    }
}
