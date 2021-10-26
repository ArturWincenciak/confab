using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetSubmissionHandler : IQueryHandler<GetSubmission, GetSubmission.Result>
    {
        private readonly DbSet<Submission> _submissions;

        public GetSubmissionHandler(AgendasDbContext dbContext)
        {
            _submissions = dbContext.Submissions;
        }

        public async Task<GetSubmission.Result> HandleAsync(GetSubmission query)
        {
            var submission = await _submissions
                .AsNoTracking()
                .Where(x => x.Id == query.Id)
                .Include(x => x.Speakers)
                .Select(x => Map(x))
                .SingleOrDefaultAsync();

            return submission;
        }

        private static GetSubmission.Result Map(Submission entity)
        {
            return new GetSubmission.Result(entity.Id, entity.ConferenceId, entity.Title, entity.Description,
                entity.Level, entity.Status, entity.Tags,
                entity.Speakers.Select(x => new GetSubmission.Result.SpeakerDto(x.Id, x.FullName)));
        }
    }
}