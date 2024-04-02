using System;
using System.Collections.Generic;

namespace Confab.Shared.Kernel.Types.Base;

public class AggregateId<T> : IEquatable<AggregateId<T>>
{
    public T Value { get; }

    public AggregateId(T value) =>
        Value = value;

    public bool Equals(AggregateId<T> other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(objA: this, other))
            return true;

        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(objA: this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((AggregateId<T>) obj);
    }

    public override int GetHashCode() =>
        EqualityComparer<T>.Default.GetHashCode(Value);

    public static implicit operator T(AggregateId<T> id) =>
        id.Value;

    public static implicit operator AggregateId<T>(T id) =>
        new(id);

    public static bool operator ==(AggregateId<T> objOne, AggregateId<T> objTwo) =>
        objOne.Equals(objTwo);

    public static bool operator !=(AggregateId<T> objOne, AggregateId<T> objTwo) =>
        !(objOne == objTwo);
}

public class AggregateId : AggregateId<Guid>
{
    public AggregateId(Guid value)
        : base(value)
    {
    }

    public static implicit operator Guid(AggregateId id) =>
        id.Value;

    public static implicit operator AggregateId(Guid id) =>
        new(id);
}