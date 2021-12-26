using System;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Attendances.Domain.Types
{
    public class UserId : TypeId
    {
        public UserId(Guid id)
            : base(id)
        {
        }

        public static implicit operator UserId(Guid id)
            => new(id);
    }
}