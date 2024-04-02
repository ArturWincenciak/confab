﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories;

internal sealed class AgendaItemRepository : IAgendaItemRepository
{
    private readonly DbSet<AgendaItem> _agendaItems;
    private readonly AgendasDbContext _context;

    public AgendaItemRepository(AgendasDbContext context)
    {
        _context = context;
        _agendaItems = context.AgendaItems;
    }

    public async Task<IEnumerable<AgendaItem>> BrowsAsync(IEnumerable<SpeakerId> speakerIds)
    {
        var ids = speakerIds.Select(x => (Guid) x).ToList();
        return await _agendaItems
            .AsNoTracking()
            .Include(x => x.Speakers)
            .Where(x => x.Speakers.Any(x => ids.Contains(x.Id)))
            .ToListAsync();
    }

    public async Task<AgendaItem> GetAsync(AggregateId id)
    {
        return await _agendaItems
            .Include(x => x.Speakers)
            .Include(x => x.AgendaSlot)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(AgendaItem entity)
    {
        _context.AttachRange(entity.Speakers);
        _agendaItems.Add(entity);
        await _context.SaveChangesAsync();
    }
}