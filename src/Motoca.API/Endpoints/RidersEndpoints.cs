using Microsoft.AspNetCore.Http.HttpResults;

namespace Motoca.API.Endpoints;

#pragma warning disable 1591
public class RidersEndpointsServices(IMediator mediator,
                                     ILogger<RidersEndpointsServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;

    public ILogger<RidersEndpointsServices> Logger { get; } = logger;
}

public static class RidersEndpoints
{
    private const string TagEndpoint = "Entregadores";
    private const string BaseEndpoint = "entregadores";

    public static void MapRidersEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup(BaseEndpoint)
                     .WithTags(TagEndpoint);

        // baseEndpoint.MapGet("/", () =>
        // {
        //     return "";
        // });

        // baseEndpoint.MapGet("/{id}", (int id) =>
        // {
        //     return id;
        // });

        api.MapPost("/", () =>
        {
            return "";
        });

        api.MapPost("/{id}/cnh", (int id) =>
        {
            return id;

        });

        // baseEndpoint.MapDelete("/{id}", (int id) =>
        // {
        //     return id;
        // });
    }

    // /// <summary>
    // /// Consulta uma moto cadastrada pelo Id (Identificador)
    // /// </summary>
    // /// <param name="id">Id da moto</param>
    // /// <param name="services"></param>
    // private static async Task<Results<Created<Rental[]>, BadRequest<string>>> GetRentalByIdAsync(
    //    [FromRoute(Name = "id")] string id,
    //    [AsParameters] RentalsEndpointsServices services)
    // {
    //     try
    //     {
    //         var query = new GetRentalByIdQuery(id);
    //         var rentals = await services.Mediator.Send(query);

    //         return TypedResults.Created(string.Empty, rentals);
    //     }
    //     catch (Exception ex)
    //     {
    //         return TypedResults.BadRequest(ex.Message);
    //     }
    // }
}
