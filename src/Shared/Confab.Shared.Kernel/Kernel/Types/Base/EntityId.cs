﻿using System;

namespace Confab.Shared.Abstractions.Kernel.Types.Base
{
    public class EntityId : TypeId
    {
        public EntityId(Guid id)
            : base(id)
        {
        }

        public static implicit operator EntityId(Guid id)
        {
            return new EntityId(id);
        }
    }
}