using Motoca.API.Application.Rentals.Models;

namespace Motoca.API.Application.Rentals.Queries;

#pragma warning disable 1591
public record GetRentalsQuery() : IRequest<Rental[]>;
