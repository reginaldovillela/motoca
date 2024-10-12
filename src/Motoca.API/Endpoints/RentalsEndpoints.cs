namespace Motoca.API.Endpoints;

#pragma warning disable 1591
public static class RentalsEndpoints
{
    private const string TagEndpoint = "Locações";
    private const string BaseEndpoint = "locacoes";

    public static void MapRentalsEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup(BaseEndpoint)
                     .WithTags(TagEndpoint);

        api.MapGet("/{id}", (int id) =>
        {
            return id;
        });

        api.MapPost("/", () =>
        {
            return "";
        });

        api.MapPut("/{id}/devolucao", (int id) =>
        {
            return id;

        });
    }
}
