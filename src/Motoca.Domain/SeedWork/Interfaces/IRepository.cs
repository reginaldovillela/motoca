namespace Motoca.Domain.SeedWork.Interfaces;

/// <summary>
/// Interface que implementa o padrão Repository. É o contrato base para as interfaces de repositórios
/// </summary>
/// <typeparam name="T">Determina a entidade do repositório</typeparam>
/// <see href="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces#repository-contracts-interfaces-in-the-domain-model-layer">Documentação MS</see>
#pragma warning disable S2326 // Unused type parameters should be removed
public interface IRepository<T> where T : IAggregateRoot
#pragma warning restore S2326 // Unused type parameters should be removed
{
    IUnitOfWork UnitOfWork { get; }
}
