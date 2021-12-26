using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Modules.Agendas.Domain.Submissions.Events;
using Confab.Shared.Kernel;

namespace Confab.Modules.Agendas.Domain.Agendas.Events.Handlers
{
    internal sealed class SubmissionApprovedHandler : IDomainEventHandler<SubmissionApproved>
    {
        private readonly IAgendaItemRepository _agendaItemRepository;

        public SubmissionApprovedHandler(IAgendaItemRepository agendaItemRepository)
        {
            _agendaItemRepository = agendaItemRepository;
        }

        public async Task HandleAsync(SubmissionApproved @event)
        {
            var submission = @event.Submission;
            var agendaItem = await _agendaItemRepository.GetAsync(submission.Id);
            var isAgendaItemExists = agendaItem is not null;
            if (isAgendaItemExists)
                return;

            var newAgendaItem = AgendaItem.Create(submission.Id, submission.ConferenceId, submission.Title,
                submission.Description, submission.Level, submission.Tags, submission.Speakers);
            await _agendaItemRepository.AddAsync(newAgendaItem);
        }
    }
}