using System;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Shared.Kernel.Types;

public class ConferenceId : TypeId
{
    public ConferenceId(Guid id)
        : base(id)
    {
    }

    public static implicit operator ConferenceId(Guid id) =>
        new(id);
}