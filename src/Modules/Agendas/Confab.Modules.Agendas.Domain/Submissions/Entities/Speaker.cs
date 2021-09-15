using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public sealed class Speaker : AggregateRoot
    {
        public Speaker(AggregateId id, string fullName)
        {
            (Id, FullName) = (id, fullName);
        }

        public string FullName { get; init; }
        public ICollection<Submission> Submissions { get; }

        public static Speaker Create(Guid id, string fullName)
        {
            return new Speaker(id, fullName);
        }
    }
}