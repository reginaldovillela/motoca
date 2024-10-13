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
    }

    /// <summary>
    /// Consulta uma locação cadastrada no sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da locação</param>
    /// <param name="services"></param>
    private static async Task<Results<Created<Rental[]>, BadRequest<AnyFailureResult>>> GetRentalByIdAsync(
       [FromRoute(Name = "id")] string id,
       [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            var query = new GetRentalByIdQuery(id);
            var rentals = await services.Mediator.Send(query);

            return TypedResults.Created(string.Empty, rentals);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
    }

    /// <summary>
    /// Cadastra uma moto no sistema
    /// </summary>
    /// <param name="command">Dados da moto para cadastrar</param>
    /// <param name="services"></param>
    private static async Task<Results<Created<Rental>, BadRequest<AnyFailureResult>>> CreateRentalAsync(
        [FromBody] CreateRentalCommand command,
        [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            var rental = await services.Mediator.Send(command);

            return TypedResults.Created(string.Empty, rental);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }

    /// <summary>
    /// Altera a placa de uma moto cadastrada no sistema pelo Id (Identificador)
    /// </summary>
    /// <param name="id">Id da moto</param>
    /// <param name="command">Dados da nova placa</param>
    /// <param name="services"></param>
    private static async Task<Results<Ok<AnySuccessWithDataResult<Rental>>, BadRequest<AnyFailureResult>>> EndRentalAsync(
        [FromRoute(Name = "id")] string id,
        [FromBody] EndRentalCommand command,
        [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            //command.Id = id;

            var rental = await services.Mediator.Send(command);

            return TypedResults.Ok(new AnySuccessWithDataResult<Rental>("Data de devolução informada com sucesso", rental));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Dados inválidos", ex.Message));
        }
    }
}