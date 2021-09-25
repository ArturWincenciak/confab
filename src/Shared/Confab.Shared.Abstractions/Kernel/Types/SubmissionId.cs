using System;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public class SubmissionId : AggregateId
    {
        public SubmissionId(Guid value)
            : base(value)
        {
        }
    }
}