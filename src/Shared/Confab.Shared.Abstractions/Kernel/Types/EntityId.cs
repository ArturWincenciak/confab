using System;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public class EntityId : TypeId
    {
        public EntityId(Guid id)
            : base(id)
        {
        }

        public static implicit operator EntityId(Guid id)
        {
            return new(id);
        }
    }
}