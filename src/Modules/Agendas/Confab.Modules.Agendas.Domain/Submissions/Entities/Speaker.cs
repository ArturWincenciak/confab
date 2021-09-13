using System;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    internal sealed class Speaker : AggregateRoot
    {
        public Speaker(AggregateId id, string fullName)
        {
            (Id, FullName) = (id, fullName);
        }

        public string FullName { get; }

        public static Speaker Create(Guid id, string fullName)
        {
            return new Speaker(id, fullName);
        }
    }
}