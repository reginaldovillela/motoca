using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Riders.Queries;

#pragma warning disable 1591
public record GetRidersQuery : IRequest<Rider[]>;