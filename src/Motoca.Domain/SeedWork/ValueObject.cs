using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.SeedWork;

//https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces
//https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects

public abstract class ValueObject : IValueObject
{
    protected static bool EqualOperator(ValueObject objA, ValueObject objB)
    {
        if (objA is null ^ objB is null)
            return false;

        return ReferenceEquals(objA, objB) || objA!.Equals(objB);
    }

    protected static bool NotEqualOperator(ValueObject objA, ValueObject objB)
    {
        return !EqualOperator(objA, objB);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var objB = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(objB.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject objA, ValueObject objB)
    {
        return EqualOperator(objA, objB);
    }

    public static bool operator !=(ValueObject objA, ValueObject objB)
    {
        return NotEqualOperator(objA, objB);
    }
}