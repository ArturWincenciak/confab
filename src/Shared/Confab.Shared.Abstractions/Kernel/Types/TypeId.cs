using System;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public abstract class TypeId : IEquatable<TypeId>
    {
        protected TypeId(Guid id)
        {
            Value = id;
        }

        public Guid Value { get; }

        public bool IsEmpty => Value == Guid.Empty;

        public bool Equals(TypeId other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((TypeId) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator Guid(TypeId id)
        {
            return id.Value;
        }

        public static bool operator ==(TypeId a, TypeId b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(TypeId a, TypeId b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}