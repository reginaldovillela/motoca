namespace Motoca.SharedKernel.Message;

public class GetBikeHasRentalsResponse
{
    public string? ErrorMessage { get; init; }

    public bool HasRentals { get; init; }
}
