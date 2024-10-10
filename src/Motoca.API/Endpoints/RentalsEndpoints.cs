using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoca.API.Endpoints;

public static class RentalsEndpoints
{
    private const string TagEndpoint = "Locações";
    private const string BaseEndpoint = "locacoes";

    public static void MapRentalsEndpoints(this WebApplication app)
    {
        var baseEndpoint = app.MapGroup(BaseEndpoint)
                              .WithTags(TagEndpoint);

        // baseEndpoint.MapGet("/", () =>
        // {

        //     return "";
        // });

        baseEndpoint.MapGet("/{id}", (int id) =>
        {
            return id;
        });

        baseEndpoint.MapPost("/", () =>
        {
            return "";
        });

        baseEndpoint.MapPut("/{id}/devolucao", (int id) =>
        {
            return id;

        });

        // baseEndpoint.MapDelete("/{id}", (int id) =>
        // {
        //     return id;
        // });
    }
}
