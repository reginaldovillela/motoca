namespace Motoca.API.Models.Results;

/// <summary>
/// Modelo para qualquer sucesso
/// </summary>
/// <param name="message">Mensagem para o cliente</param>
public class AnySuccessResult(string message)
{
    /// <summary>
    /// Mensagem para o cliente
    /// </summary>
    [JsonPropertyName("mensagem")]
    [Required]
    public string Message { get; init; } = message;
}
