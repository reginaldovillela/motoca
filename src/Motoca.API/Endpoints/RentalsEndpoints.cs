using Motoca.API.Application.Rentals.Commands;
using Motoca.API.Application.Rentals.Models;
using Motoca.API.Application.Rentals.Queries;
using Motoca.API.Models.Results;

namespace Motoca.API.Endpoints;

#pragma warning disable 1591
public class RentalsEndpointsServices(IMediator mediator,
                                      ILogger<RentalsEndpointsServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;

    public ILogger<RentalsEndpointsServices> Logger { get; } = logger;
}

public static class RentalsEndpoints
{
    private const string TagEndpoint = "Locações";
    private const string BaseEndpoint = "locacoes";

    public static void MapRentalsEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup(BaseEndpoint)
                     .WithTags(TagEndpoint);

        api.MapGet("/{id}", GetRentalByIdAsync);

        api.MapPost("/", CreateRentalAsync);

        api.MapPut("/{id}/devolucao", EndRentalAsync);

        api.MapGet("/planos", GetPlansAsync);
    }

    /// <summary>
    /// Consulta uma locação cadastrada no sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da locação</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<Rental>,
                                      NotFound<AnyFailureResult>,
                                      BadRequest<AnyFailureResult>>> GetRentalByIdAsync(
       [FromRoute(Name = "id")] string id,
       [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            var query = new GetRentalByIdQuery(id);
            var rental = await services.Mediator.Send(query);

            return TypedResults.Ok(rental);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
    }

    /// <summary>
    /// Cadastra uma locação no sistema
    /// </summary>
    /// <param name="command">Dados da locação para cadastrar</param>
    /// <param name="services"></param>
    private static async Task<Results<Created<Rental>,
                             BadRequest<AnyFailureResult>>> CreateRentalAsync(
        [FromBody] CreateRentalCommand command,
        [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            var rental = await services.Mediator.Send(command);

            //  if (rental is null || rental.Length == 0)
            //     return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", "Não encontramos nenhuma moto cadastrada no sistema"));

            return TypedResults.Created(string.Empty, rental);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }

    /// <summary>
    /// Finaliza uma locação cadastrada no sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da locação</param>
    /// <param name="command">Dados para finalizar a locação</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<AnySuccessWithDataResult<Rental>>,
                                      NotFound<AnyFailureResult>,
                                      BadRequest<AnyFailureResult>>> EndRentalAsync(
        [FromRoute(Name = "id")] string id,
        [FromBody] EndRentalCommand command,
        [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            command.Id = id;

            var rental = await services.Mediator.Send(command);

            if (rental is null)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", "Não encontramos nenhuma locação cadastrada no sistema"));

            return TypedResults.Ok(new AnySuccessWithDataResult<Rental>("Data de devolução informada com sucesso", rental));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }


    /// <summary>
    /// Busca todos os planos cadastrados no sistema
    /// </summary>
    /// <param name="services"></param>
    private static async Task<Results<Ok<Plan[]>,
                                      NotFound<AnyFailureResult>,
                                      BadRequest<AnyFailureResult>>> GetPlansAsync(
       [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            var query = new GetPlansQuery();
            var plans = await services.Mediator.Send(query);

            return TypedResults.Ok(plans);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
    }
}
