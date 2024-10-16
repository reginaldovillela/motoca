namespace Motoca.Domain.SeedWork.Interfaces;

/// <summary>
/// Tagging Interface
/// </summary>
/// <see href="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces">Documentação MS</see>
public interface IEntity
{
    void AddDomainEvent(INotification eventItem);

    void RemoveDomainEvent(INotification eventItem);

    void ClearDomainEvents();
};
