using System;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Attendances.Domain.Types;

public class ParticipantId : TypeId
{
    public ParticipantId(Guid id)
        : base(id)
    {
    }

    public static implicit operator ParticipantId(Guid id) =>
        new(id);
}