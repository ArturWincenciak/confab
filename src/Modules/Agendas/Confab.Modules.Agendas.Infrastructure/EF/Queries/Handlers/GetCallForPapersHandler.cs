using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForProps.Queries;
using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetCallForPapersHandler : IQueryHandler<GetCallForPapers, GetCallForPapers.Result>
    {
        private readonly DbSet<CallForPapers> _callForPapers;

        public GetCallForPapersHandler(AgendasDbContext context)
        {
            _callForPapers = context.CallForPapers;
        }

        public async Task<GetCallForPapers.Result> HandleAsync(GetCallForPapers query)
        {
            return await _callForPapers
                .AsNoTracking()
                .Where(x => x.ConferenceId == query.ConferenceId)
                .Select(x => AsDto(x))
                .SingleOrDefaultAsync();
        }

        private static GetCallForPapers.Result AsDto(CallForPapers callForPapers)
        {
            return new GetCallForPapers.Result
            (
                callForPapers.Id,
                callForPapers.ConferenceId,
                callForPapers.From,
                callForPapers.To,
                callForPapers.IsOpened
            );
        }
    }
}