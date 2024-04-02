using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.CallForProps.Events;

internal record CallForPaperOpened(Guid ConferenceId) : IEvent;