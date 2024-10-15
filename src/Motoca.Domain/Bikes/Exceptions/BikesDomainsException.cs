namespace Motoca.Domain.Bikes.Exceptions;

public class BikesDomainsException : Exception
{
    public Guid EntityId { get; set; }
    public string Id { get; set; }

    public BikesDomainsException(Guid entityId, string id)
    {
        EntityId = entityId;
        Id = id;
    }

    public BikesDomainsException(Guid entityId, string id, string message)
        : base(message)
    {
        EntityId = entityId;
        Id = id;
    }

    public BikesDomainsException(Guid entityId, string id, string message, Exception innerException)
        : base(message, innerException)
        
    {
        EntityId = entityId;
        Id = id;
    }
}
