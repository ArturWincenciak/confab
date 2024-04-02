using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Attendances.Domain.Entities;

namespace Confab.Modules.Attendances.Domain.Policies;

public class RegularSlotPolicy : ISlotPolicy
{
    public IEnumerable<Slot> Generate(int participantsLimit)
    {
        return Enumerable.Range(start: 0, participantsLimit).Select(x => new Slot(Guid.NewGuid()));
    }
}