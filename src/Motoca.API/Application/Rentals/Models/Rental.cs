using System.ComponentModel;

namespace Motoca.API.Application.Rentals.Models;

/// <summary>
/// Dados da locação cadastrada no sistema
/// </summary>
/// <param name="uid"></param>
public class Rental(Guid uid)
{
    /// <summary>
    /// Id (Identificador) da locação cadastrada no sistema
    /// </summary>
    [DefaultValue("rental-123")]
    [JsonPropertyName("identificador")]
    public Guid Uid { get; init; } = uid;
}
