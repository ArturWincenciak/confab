using System;

namespace Confab.Shared.Abstractions
{
    public interface IClock
    {
        DateTime CurrentDate();
    }
}