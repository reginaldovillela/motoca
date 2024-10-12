using System.ComponentModel.DataAnnotations;

namespace Motoca.API.Application.Bikes.Models;

/// <summary>
/// Dados da moto
/// </summary>
/// <param name="Identificador">Id (Identificador) da moto cadastrada no sistema</param>
/// <param name="Ano">Ano da moto cadastrada no sistema</param>
/// <param name="Modelo">Modelo da moto cadastrada no sistema</param>
/// <param name="Placa">Placa da moto cadastrada no sistema</param>
public record Bike([Required] string Identificador, ushort Ano, string Modelo, string Placa);
