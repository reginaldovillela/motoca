namespace Motoca.API.Models.Results;

/// <summary>
/// Modelo para qualquer erro
/// </summary>
/// <param name="message">Mensagem para o cliente</param>
/// <param name="error">Erro detalhado</param>
public class AnyFailureResult(string message, string error)
{
    /// <summary>
    /// Mensagem para o cliente
    /// </summary>
    [JsonPropertyName("mensagem")]
    [Required]
    public string Message { get; init; } = message;

    /// <summary>
    /// Erro detalhado
    /// </summary>
    [JsonPropertyName("erro")]
    [Required]
    public string Error { get; init; } = error;
}
