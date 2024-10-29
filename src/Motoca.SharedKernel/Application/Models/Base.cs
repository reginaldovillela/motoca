namespace Motoca.SharedKernel.Application.Models;

/// <summary>
/// Base para outros modelos
/// </summary>
/// <param name="InternalId">Id Interno do sistema</param>
public abstract record Base(
    [property: JsonIgnore, JsonPropertyName("internal_id"), Required] Guid InternalId);