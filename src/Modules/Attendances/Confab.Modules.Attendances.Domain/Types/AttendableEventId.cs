using System;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Attendances.Domain.Types
{
    public class AttendableEventId : TypeId
    {
        public AttendableEventId(Guid id)
            : base(id)
        {
        }

        public static implicit operator AttendableEventId(Guid id)
            => new(id);
    }
}