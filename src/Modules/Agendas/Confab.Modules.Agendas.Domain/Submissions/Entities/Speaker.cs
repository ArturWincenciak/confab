using System;
using System.Collections.Generic;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public class Speaker : AggregateRoot
    {
        public string FullName { get; private set; }
        public ICollection<Submission> Submissions { get; private set; } //TU
        public AgendaItem AgendaItem { get; private set; } //TU

        public static Speaker Create(AggregateId id, string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new EmptyFullnameOfSpeakerException(id);

            return new Speaker
            {
                Id = id,
                FullName = fullName
            };
        }
    }
}