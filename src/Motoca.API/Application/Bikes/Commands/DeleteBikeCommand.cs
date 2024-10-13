namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public record DeleteBikeCommand(
    [property:JsonIgnore] string Id
) : IRequest<bool>;

