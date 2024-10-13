using Motoca.API.Application.Bikes.Models;

namespace Motoca.API.Application.Bikes.Queries;

#pragma warning disable 1591
public record GetBikeByIdQuery(
    [property:JsonIgnore] string Id
)  : IRequest<Bike>;