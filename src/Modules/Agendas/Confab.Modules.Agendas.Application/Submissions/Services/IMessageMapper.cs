using System.Collections.Generic;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Kernel;

namespace Confab.Modules.Agendas.Application.Submissions.Services;

internal interface IMessageMapper
{
    IMessage Map(IDomainEvent @event);
    IEnumerable<IMessage> Map(IEnumerable<IDomainEvent> events);
}