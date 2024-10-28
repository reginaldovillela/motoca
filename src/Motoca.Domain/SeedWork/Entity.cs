using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.SeedWork;

/// <summary>
/// Classe que implementa o padrão Entity. Serve para colocar em outras entidades.
/// Seguido o padrão MS com algumas modicaficações.
/// </summary>
/// <see href="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces#the-custom-entity-base-class">Documentação MS</see>
public abstract class Entity : IEntity
{
    int? _requestedHashCode;

    private List<INotification> _domainEvents;

    [Key]
    [Column("InternalId")]
    public virtual Guid EntityId { get; protected set; }

    [NotMapped]
    public List<INotification> DomainEvents => _domainEvents;

    protected Entity()
    {
        EntityId = Guid.NewGuid();
        _domainEvents = [];
    }

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents ??= [];
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        if (_domainEvents is null)
            return;

        _domainEvents.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    #region BaseBehaviours

    public override bool Equals(object? obj)
    {
        if (obj is Entity entity)
        {
            if (ReferenceEquals(this, entity))
                return true;

            return EntityId.Equals(entity.EntityId);
        }

        return false;
    }

#pragma warning disable S3875 // "operator==" should not be overloaded on reference types
    public static bool operator ==(Entity objA, Entity objB)
#pragma warning restore S3875 // "operator==" should not be overloaded on reference types
    {
        if (objA is null && objB is null)
            return true;

        if (objA is null || objB is null)
            return false;

        return objA.Equals(objB);
    }

    public static bool operator !=(Entity objA, Entity objB)
    {
        return !(objA == objB);
    }

    public override int GetHashCode()
    {
        if (!_requestedHashCode.HasValue)
            _requestedHashCode = EntityId.GetHashCode() ^ 31;
       
        return _requestedHashCode.Value;
    }

    public override string ToString()
    {
        return $"{GetType().Name} [EntityId={EntityId}]";
    }

    #endregion
}