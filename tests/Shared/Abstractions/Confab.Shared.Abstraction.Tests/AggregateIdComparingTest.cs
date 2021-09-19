using System;
using Confab.Shared.Abstractions.Kernel.Types;
using Xunit;

namespace Confab.Shared.Abstraction.Tests
{
    public class AggregateIdComparingTest
    {
        [Fact]
        public void
            GIVEN_guid_id_type_and_entity_WHEN_compare_the_id_with_entity_id_using_equal_operator_THEN_they_are_equal()
        {
            // arrange
            var (id, entity) = ArrangeTheSameIdValue();

            // act
            var areEqual = id == entity.Id;

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public void
            GIVEN_exactly_the_same_aggregate_id_type_and_entity_WHEN_compare_the_id_with_entity_id_using_equal_method_THEN_they_are_equal()
        {
            // arrange
            var (id, entity) = ArrangeExactlyTheSameTypeOfIdAndTheSameIdValueWithDifferentReferences();

            // act
            var areEqual = id.Equals(entity.Id);

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public void
            GIVEN_exactly_the_same_aggregate_id_type_and_entity_WHEN_compare_the_id_with_entity_id_using_equal_operator_THEN_they_are_equal()
        {
            // arrange
            var (id, entity) = ArrangeExactlyTheSameTypeOfIdAndTheSameIdValueWithDifferentReferences();

            // act
            var areEqual = id == entity.Id;

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_and_entity_WHEN_compare_entity_id_with_the_id_using_equal_operator_THEN_they_are_equal()
        {
            // arrange
            var (id, entity) = ArrangeTheSameIdValue();

            // act
            var areEqual = entity.Id == id;

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_and_entity_WHEN_compare_the_id_with_entity_id_using_equal_method_THEN_they_are_equal()
        {
            // arrange
            var (id, entity) = ArrangeTheSameIdValue();

            // act
            // there is implicit casting from AggregateId to Guid
            var areEqual = id.Equals(entity.Id);

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_and_entity_WHEN_compare_entity_id_with_the_id_using_equal_method_THEN_they_are_equal()
        {
            // arrange
            var (id, entity) = ArrangeTheSameIdValue();

            // act
            // there is no implicit casting from Guid to AggregateId
            var areEqual = entity.Id.Equals(id);

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_and_entity_WHEN_compare_entity_id_with_cast_id_using_equal_method_THEN_they_are_equal()
        {
            // arrange
            var (id, entity) = ArrangeTheSameIdValue();

            // act
            // there is explicit casting from Guid to AggregateId
            var areEqual = entity.Id.Equals((AggregateId) id);

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_with_wrong_value_and_entity_WHEN_compare_the_id_with_entity_using_equal_operator_THEN_not_equal()
        {
            // arrange
            var (id, entity) = ArrangeDifferentIdValue();

            // act
            var areEqual = id == entity.Id;

            // assert
            Assert.False(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_with_wrong_value_and_entity_WHEN_compare_entity_with_the_id_using_equal_operator_THEN_not_equal()
        {
            // arrange
            var (id, entity) = ArrangeDifferentIdValue();

            // act
            var areEqual = entity.Id == id;

            // assert
            Assert.False(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_with_wrong_value_and_entity_WHEN_compare_the_id_with_entity_using_equal_method_THEN_not_equal()
        {
            // arrange
            var (id, entity) = ArrangeDifferentIdValue();

            // act
            var areEqual = id.Equals(entity.Id);

            // assert
            Assert.False(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_with_wrong_value_and_entity_WHEN_compare_entity_with_the_id_using_equal_method_THEN_not_equal()
        {
            // arrange
            var (id, entity) = ArrangeDifferentIdValue();

            // act
            var areEqual = entity.Id.Equals(id);

            // assert
            Assert.False(areEqual);
        }

        [Fact]
        public void
            GIVEN_guid_id_type_with_wrong_value_and_entity_WHEN_compare_entity_with_cast_id_using_equal_method_THEN_not_equal()
        {
            // arrange
            var (id, entity) = ArrangeDifferentIdValue();

            // act
            var areEqual = entity.Id.Equals((AggregateId) id);

            // assert
            Assert.False(areEqual);
        }

        [Fact]
        public void
            GIVEN_different_int_id_type_and_entity_type_WHEN_compare_entity_with_the_id_using_equal_method_THEN_not_equal()
        {
            // arrange
            var (id, entity) = ArrangeDifferentIdType();

            // act
            var areEqual = entity.Id.Equals(id);

            // assert
            Assert.False(areEqual);
        }

        private (Guid id, AggregateRoot enity) ArrangeTheSameIdValue()
        {
            var id = Guid.Parse("EE07E9AD-61AE-446D-AA40-D602CFA461F3");
            var entity = new AggregateRootMock(id);
            return (id, entity);
        }

        private (Guid id, AggregateRoot enity) ArrangeDifferentIdValue()
        {
            var id = Guid.Parse("779F525A-F0C6-45BF-9E16-AAF559EDAD18");
            var anotherId = Guid.Parse("47A3D698-7733-4EA4-AC8E-A22AA61DC279");
            var entity = new AggregateRootMock(anotherId);
            return (id, entity);
        }

        private (int id, AggregateRoot enity) ArrangeDifferentIdType()
        {
            var id = 0;
            var anotherId = Guid.Parse("47A3D698-7733-4EA4-AC8E-A22AA61DC279");
            var entity = new AggregateRootMock(anotherId);
            return (id, entity);
        }

        private static (AggregateId, AggregateRoot enity) ArrangeExactlyTheSameTypeOfIdAndTheSameIdValueWithDifferentReferences()
        {
            var guid = "EE07E9AD-61AE-446D-AA40-D602CFA461F3";
            var id = new AggregateId(Guid.Parse(guid));
            var entity = new AggregateRootMock(new AggregateId(Guid.Parse(guid)));
            return (id, entity);
        }

        private sealed class AggregateRootMock : AggregateRoot
        {
            public AggregateRootMock(AggregateId id)
            {
                Id = id;
            }
        }
    }
}