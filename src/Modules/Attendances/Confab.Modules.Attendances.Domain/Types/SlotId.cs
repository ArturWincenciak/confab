using System;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Attendances.Domain.Types
{
    public class SlotId : TypeId
    {
        public SlotId(Guid id)
            : base(id)
        {
        }

        public static implicit operator SlotId(Guid id)
            => new(id);
    }
}