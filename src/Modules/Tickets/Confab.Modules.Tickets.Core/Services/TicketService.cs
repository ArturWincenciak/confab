﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.DTO;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Events;
using Confab.Modules.Tickets.Core.Exceptions;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstractions;
using Confab.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Tickets.Core.Services;

internal class TicketService : ITicketService
{
    private readonly IClock _clock;
    private readonly IConferenceRepository _conferenceRepository;
    private readonly ITicketGenerator _generator;
    private readonly ILogger<TicketService> _logger;
    private readonly IMessageBroker _messageBroker;
    private readonly ITicketSaleRepository _saleRepository;
    private readonly ITicketRepository _ticketRepository;

    public TicketService(IClock clock, IConferenceRepository conferenceRepository,
        ITicketRepository ticketRepository, ITicketSaleRepository ticketSaleRepository, ITicketGenerator generator,
        ILogger<TicketService> logger, IMessageBroker messageBroker)
    {
        _clock = clock;
        _conferenceRepository = conferenceRepository;
        _ticketRepository = ticketRepository;
        _saleRepository = ticketSaleRepository;
        _generator = generator;
        _logger = logger;
        _messageBroker = messageBroker;
    }

    public async Task PurchaseAsync(Guid conferenceId, Guid userId)
    {
        var conference = await _conferenceRepository.GetAsync(conferenceId);
        if (conference is null)
            throw new ConferenceNotFoundException(conferenceId);

        var ticket = await _ticketRepository.GetAsync(conferenceId, userId);
        if (ticket is not null)
            throw new TicketAlreadyPurchasedException(conferenceId, userId);

        var now = _clock.CurrentDate();
        var ticketSale = await _saleRepository.GetCurrentForConferencesIncludingTicketsAsync(conferenceId, now);
        if (ticketSale is null)
            throw new TicketSaleUnavailableException(conferenceId);

        if (ticketSale.Amount.HasValue)
        {
            await PurchaseAvailableAsync(ticketSale, userId, ticketSale.Price, now);
            return;
        }

        ticket = _generator.Generate(conferenceId, ticketSale.Id, ticketSale.Price);
        ticket.Purchase(userId, now, ticketSale.Price);
        await _ticketRepository.AddAsync(ticket);
        await _messageBroker.PublishAsync(new TicketPurchased(ticket.Id, conferenceId, userId));
    }

    public async Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId)
    {
        var tickets = await _ticketRepository.GetForUserIncludingConferenceAsync(userId);

        return tickets.Select(x => new TicketDto(x.Code, x.Price, x.PurchasedAt.Value,
                Conference: new(x.ConferenceId, x.Conference.Name)))
            .OrderBy(x => x.PurchasedAt);
    }

    private async Task PurchaseAvailableAsync(TicketSale ticketSale, Guid userId, decimal? price,
        DateTime timestamp)
    {
        var conferenceId = ticketSale.ConferenceId;
        var ticket = ticketSale.Tickets
            .Where(x => x.UserId is null)
            .OrderBy(_ => Guid.NewGuid())
            .FirstOrDefault();

        if (ticket is null)
            throw new TicketSaleUnavailableException(conferenceId);

        ticket.Purchase(userId, timestamp, price);
        await _ticketRepository.UpdateAsync(ticket);
        await _messageBroker.PublishAsync(new TicketPurchased(ticket.Id, conferenceId, userId));
    }
}