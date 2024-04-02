using System;
using Confab.Shared.Abstractions;

namespace Confab.Shared.Infrastructure.Time;

internal class UtcClock : IClock
{
    public DateTime CurrentDate() =>
        DateTime.UtcNow;
}