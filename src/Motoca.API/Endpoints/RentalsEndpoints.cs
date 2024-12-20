using Motoca.API.Application.Rentals.Commands;
using Motoca.API.Application.Rentals.Queries;
using Motoca.API.Filters;
using Motoca.API.Models.Results;
using Motoca.SharedKernel.Application.Models;

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

        api.MapGet("/", GetRentalsAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable);

        api.MapGet("/{id}", GetRentalByIdAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable);

        api.MapPost("/", CreateRentalAsync)
            .AddEndpointFilter<ValidationFilter<CreateRentalCommand>>()
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity);

        api.MapPut("/{id}/devolucao", EndRentalAsync)
            .AddEndpointFilter<ValidationFilter<EndRentalCommand>>()
            .ProducesProblem((int)HttpStatusCode.NotAcceptable)
            .ProducesValidationProblem((int)HttpStatusCode.UnprocessableEntity);

        api.MapGet("/planos", GetPlansAsync)
            .ProducesProblem((int)HttpStatusCode.NotAcceptable);
    }

    /// <summary>
    /// Consulta todas as locações no sistema
    /// </summary>
    /// <param name="services"></param>
    private static async Task<Results<Ok<ICollection<Rental>>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetRentalsAsync(
        [AsParameters] RentalsEndpointsServices services)
    {
        try
        {
            var query = new GetRentalsQuery();
            var rentals = await services.Mediator.Send(query);

            if (rentals is null || rentals.Count == 0)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", "Não encontramos nenhuma locação cadastrada no sistema"));

            return TypedResults.Ok(rentals);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new AnyFailureResult("Não foi possível processar sua solicitação", ex.Message));
        }
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

            if (rental is null)
                return TypedResults.NotFound(new AnyFailureResult("Dados inválidos", $"A locação com o Id {id} não foi localizada"));

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
    private static async Task<Results<Ok<ICollection<Plan>>,
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
