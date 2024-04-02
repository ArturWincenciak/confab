using System;

namespace Confab.Shared.Kernel.Types.Base;

public abstract class TypeId : IEquatable<TypeId>
{
    public Guid Value { get; }

    public bool IsEmpty => Value == Guid.Empty;

    protected TypeId(Guid id) =>
        Value = id;

    public bool Equals(TypeId other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(objA: this, other))
            return true;

        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(objA: this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;

        return Equals((TypeId) obj);
    }

    public override int GetHashCode() =>
        Value.GetHashCode();

    public static implicit operator Guid(TypeId id) =>
        id.Value;

    public static bool operator ==(TypeId a, TypeId b) =>
        a.Equals(b);

    public static bool operator !=(TypeId a, TypeId b) =>
        !(a == b);

    public override string ToString() =>
        Value.ToString();
}