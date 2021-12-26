using System;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Shared.Kernel.Types
{
    public class SpeakerId : TypeId
    {
        public SpeakerId(Guid id)
            : base(id)
        {
        }

        public static implicit operator SpeakerId(Guid id)
        {
            return new SpeakerId(id);
        }
    }
}