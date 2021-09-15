using System;
using System.Collections.Generic;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public class AggregateId<T> : IEquatable<AggregateId<T>>
    {
        public AggregateId(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public bool Equals(AggregateId<T> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((AggregateId<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }

    public class AggregateId : AggregateId<Guid>
    {
        public AggregateId(Guid value)
            : base(value)
        {
        }

        public static implicit operator Guid(AggregateId id)
        {
            return id.Value;
        }

        public static implicit operator AggregateId(Guid id)
        {
            return new AggregateId(id);
        }
    }
}