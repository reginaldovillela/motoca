using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoca.API.Endpoints;

#pragma warning disable 1591
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
}
