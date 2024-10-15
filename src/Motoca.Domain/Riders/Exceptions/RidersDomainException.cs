namespace Motoca.Domain.Riders.Exceptions;

public class RidersDomainException : Exception
{
    public Guid EntityId { get; set; }
    public string Id { get; set; }

    public RidersDomainException(Guid entityId, string id)
    {
        EntityId = entityId;
        Id = id;
    }

    public RidersDomainException(Guid entityId, string id, string message)
        : base(message)
    {
        EntityId = entityId;
        Id = id;
    }

    public RidersDomainException(Guid entityId, string id, string message, Exception innerException)
        : base(message, innerException)

    {
        EntityId = entityId;
        Id = id;
    }
}