using System.Collections.Generic;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Exceptions;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities;

public class Speaker : AggregateRoot
{
    public string FullName { get; private set; }
    public ICollection<Submission> Submissions { get; }
    public ICollection<AgendaItem> AgendaItems { get; }

    public static Speaker Create(AggregateId id, string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new EmptyFullnameOfSpeakerException(id);

        return new()
        {
            Id = id,
            FullName = fullName
        };
    }
}