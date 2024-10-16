namespace Motoca.Domain.Bikes.Exceptions;

public class BikesDomainException : Exception
{
    public Guid EntityId { get; set; }
    public string Id { get; set; }

    public BikesDomainException(Guid entityId, string id)
    {
        EntityId = entityId;
        Id = id;
    }

    public BikesDomainException(Guid entityId, string id, string message)
        : base(message)
    {
        EntityId = entityId;
        Id = id;
    }

    public BikesDomainException(Guid entityId, string id, string message, Exception innerException)
        : base(message, innerException)
        
    {
        EntityId = entityId;
        Id = id;
    }
}
