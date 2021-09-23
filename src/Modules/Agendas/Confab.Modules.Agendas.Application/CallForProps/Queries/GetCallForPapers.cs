using System;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.CallForProps.Queries
{
    public record GetCallForPapers(Guid ConferenceId) : IQuery<GetCallForPapers.CallForPapersDto>
    {
        public record CallForPapersDto(
            Guid Id, Guid ConferenceId, DateTime From, DateTime To, bool IsOpen) : IQueryResult;
    }
}