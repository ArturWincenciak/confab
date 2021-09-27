using System.Threading.Tasks;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands.Handlers
{
    internal sealed class AssignPlaceholderAgendaSlotHandler : ICommandHandler<AssignPlaceholderAgendaSlot>
    {
        public Task HandleAsync(AssignPlaceholderAgendaSlot command)
        {
            throw new System.NotImplementedException();
        }
    }
}