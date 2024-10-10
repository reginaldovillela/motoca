using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Motoca.API.Application.Bikes.Commands;
using Motoca.API.Filters;

namespace Motoca.API.Endpoints;

public static class BikesEndpoints
{
    private const string TagEndpoint = "Motos";
    private const string BaseEndpoint = "motos";

    public static void MapBikesEndpoints(this WebApplication app)
    {
        var baseEndpoint = app.MapGroup(BaseEndpoint)
                              .WithTags(TagEndpoint);

        baseEndpoint.MapGet("/", () =>
        {

            return "";
        });

        baseEndpoint.MapGet("/{id}", (int id) =>
        {
            return id;
        });

        baseEndpoint.MapPost("/", async([AsParameters] CreateBikeCommand request, IMediator mediator) =>
        {
            await mediator.Send(request);

            return "teste";
        })
        .AddEndpointFilter<ValidationFilter<CreateBikeCommand>>();

        baseEndpoint.MapPut("/{id}/placa", (int id) =>
        {
            return id;

        });

        baseEndpoint.MapDelete("/{id}", (int id) =>
        {
            return id;
        });
    }
}
