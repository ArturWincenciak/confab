using System;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public class ConferenceId : TypeId
    {
        public ConferenceId(Guid id)
            : base(id)
        {
        }

        public static implicit operator ConferenceId(Guid id)
        {
            return new ConferenceId(id);
        }
    }
}