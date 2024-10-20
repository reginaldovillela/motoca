using Motoca.SharedKernel.Application.Models;

namespace Motoca.API.Application.Rentals.Queries;

#pragma warning disable 1591
public record GetPlansQuery() : IRequest<Plan[]>;
