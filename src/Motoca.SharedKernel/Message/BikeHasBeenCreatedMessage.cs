using Motoca.SharedKernel.Application.Models;

namespace Motoca.SharedKernel.Message;

public class BikeHasBeenCreatedMessage
{
    public string Message { get; init; } = string.Empty;

    public Bike Bike { get; init; } = null!;
}