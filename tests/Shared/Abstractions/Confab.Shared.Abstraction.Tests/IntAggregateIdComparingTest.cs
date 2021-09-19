using System;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Shared.Abstraction.Tests
{
    public class IntAggregateIdComparingTest
    {
        //todo...

        private (int id, AggregateRootMock enity) ArrangeTheSameIdValue()
        {
            var id = 1;
            var entity = new AggregateRootMock(id);
            return (id, entity);
        }

        private (int id, AggregateRootMock enity) ArrangeDifferentIdValue()
        {
            var id = 1;
            var anotherId = 2;
            var entity = new AggregateRootMock(anotherId);
            return (id, entity);
        }

        private (Guid id, AggregateRootMock enity) ArrangeDifferentIdType()
        {
            var id = Guid.Parse("47A3D698-7733-4EA4-AC8E-A22AA61DC279");
            var anotherId = 1;
            var entity = new AggregateRootMock(anotherId);
            return (id, entity);
        }

        private sealed class IntAggregateId : AggregateId<int>
        {
            public IntAggregateId(int value)
                : base(value)
            {
            }

            public static implicit operator int(IntAggregateId id)
            {
                return id.Value;
            }

            public static implicit operator IntAggregateId(int id)
            {
                return new IntAggregateId(id);
            }
        }

        private class AggregateRootMock : AggregatesRoot<IntAggregateId>
        {
            public AggregateRootMock(IntAggregateId id)
            {
                Id = id;
            }
        }
    }
}