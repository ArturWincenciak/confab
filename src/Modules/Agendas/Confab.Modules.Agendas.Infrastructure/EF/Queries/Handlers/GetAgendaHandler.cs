using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetAgendaHandler : IQueryHandler<GetAgenda, GetAgenda.AgendaDto>
    {
        public Task<GetAgenda.AgendaDto> HandleAsync(GetAgenda query)
        {
            throw new System.NotImplementedException();
        }
    }
}