﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Repositories
{
    internal interface ISpeakRepository
    {
        Task<bool> ExistsAsync(AggregateId id);
        Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids);
        Task Create(Speaker entity);
    }
}