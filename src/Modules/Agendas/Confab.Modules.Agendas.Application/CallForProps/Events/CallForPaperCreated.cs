using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.CallForProps.Events;

internal record CallForPaperCreated(Guid ConferenceId) : IEvent;