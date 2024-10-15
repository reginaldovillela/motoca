namespace Motoca.Domain.Rentals.Exceptions;

public class RentalsDomainException : Exception
{
    public Guid EntityId { get; set; }

    public RentalsDomainException(Guid entityId)
    {
        EntityId = entityId;
    }

    public RentalsDomainException(Guid entityId, string message)
        : base(message)
    {
        EntityId = entityId;
    }

    public RentalsDomainException(Guid entityId, string message, Exception innerException)
        : base(message, innerException)

    {
        EntityId = entityId;
    }
}

