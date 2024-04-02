﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Repositories;

public interface IAgendaItemRepository
{
    Task<IEnumerable<AgendaItem>> BrowsAsync(IEnumerable<SpeakerId> speakerIds);
    Task<AgendaItem> GetAsync(AggregateId id);
    Task AddAsync(AgendaItem entity);
}