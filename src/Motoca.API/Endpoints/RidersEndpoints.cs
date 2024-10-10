using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoca.API.Endpoints;

public static class RidersEndpoints
{
    private const string TagEndpoint = "Entregadores";
    private const string BaseEndpoint = "entregadores";

    public static void MapRidersEndpoints(this WebApplication app)
    {
        var baseEndpoint = app.MapGroup(BaseEndpoint)
                              .WithTags(TagEndpoint);

        // baseEndpoint.MapGet("/", () =>
        // {
        //     return "";
        // });

        // baseEndpoint.MapGet("/{id}", (int id) =>
        // {
        //     return id;
        // });

        baseEndpoint.MapPost("/", () =>
        {
            return "";
        })
        .WithDescription("TESTE")
        .WithDisplayName("ttetetet")
        .WithOpenApi();

        baseEndpoint.MapPost("/{id}/cnh", (int id) =>
        {
            return id;

        });

        // baseEndpoint.MapDelete("/{id}", (int id) =>
        // {
        //     return id;
        // });
    }
}
