using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Shared.Abstraction.Tests
{
    public class CustomKeyAggregateIdComparingTest
    {
        private class Key
        {
            public Key(string propOne, int propTwo)
            {
                PropOne = propOne;
                PropTwo = propTwo;
            }

            public string PropOne { get; private set; }
            public int PropTwo { get; private set; }
        }

        private sealed class RefAggregateId : AggregateId<Key>
        {
            public RefAggregateId(Key value)
                : base(value)
            {
            }

            public static implicit operator Key(RefAggregateId id)
            {
                return id.Value;
            }

            public static implicit operator RefAggregateId(Key id)
            {
                return new RefAggregateId(id);
            }
        }

        private class AggregateRootMock : AggregatesRoot<RefAggregateId>
        {
            public AggregateRootMock(RefAggregateId id)
            {
                Id = id;
            }
        }
    }
}