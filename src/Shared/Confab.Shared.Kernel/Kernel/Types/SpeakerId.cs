using System;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Shared.Abstractions.Kernel.Types
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