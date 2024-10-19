namespace Motoca.API.Application.Base.Models;

/// <summary>
/// Base para outros modelos
/// </summary>
/// <param name="InternalId">Id Interno do sistema</param>
public abstract record BaseModelRecord(
    [property: JsonPropertyName("internal_id"), Required] Guid InternalId);