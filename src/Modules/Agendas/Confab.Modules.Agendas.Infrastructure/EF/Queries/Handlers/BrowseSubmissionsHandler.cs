using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class BrowseSubmissionsHandler : IQueryHandler<BrowseSubmissions, BrowseSubmissions.Result>
    {
        private readonly DbSet<Submission> _submissions;

        public BrowseSubmissionsHandler(AgendasDbContext dbContext)
        {
            _submissions = dbContext.Submissions;
        }

        public async Task<BrowseSubmissions.Result> HandleAsync(BrowseSubmissions query)
        {
            var submissions = _submissions
                .AsNoTracking()
                .Include(x => x.Speakers)
                .AsQueryable();

            if (query.ConferenceId.HasValue)
                submissions = submissions.Where(x => x.ConferenceId == query.ConferenceId);

            if (query.SpeakerId.HasValue)
                submissions = submissions.Where(x => x.Speakers.Any(y => y.Id == query.SpeakerId));

            var submissionsDto = await submissions
                .Select(x => AsDto(x))
                .ToListAsync();

            return new BrowseSubmissions.Result(submissionsDto);
        }

        private static BrowseSubmissions.Result.SubmissionDto AsDto(Submission submission)
        {
            return new BrowseSubmissions.Result.SubmissionDto
            (
                submission.Id,
                submission.ConferenceId,
                submission.Title,
                submission.Description,
                submission.Level,
                submission.Status,
                submission.Tags,
                submission.Speakers.Select(AsDto)
            );
        }

        private static BrowseSubmissions.Result.SubmissionDto.SpeakerDto AsDto(Speaker submissionSpeaker)
        {
            return new BrowseSubmissions.Result.SubmissionDto.SpeakerDto(submissionSpeaker.Id,
                submissionSpeaker.FullName);
        }
    }
}