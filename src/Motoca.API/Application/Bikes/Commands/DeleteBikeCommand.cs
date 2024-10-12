namespace Motoca.API.Application.Bikes.Commands;

#pragma warning disable 1591
public record DeleteBikeCommand(string Id) : IRequest<bool>;

