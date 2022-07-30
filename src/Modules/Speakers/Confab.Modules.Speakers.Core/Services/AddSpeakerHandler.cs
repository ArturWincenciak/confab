using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Speakers.Core.Services
{
    internal sealed class AddSpeakerHandler : IQueryHandler<SpeakerDto, Null>
    {
        private readonly ISpeakerService _speakerService;

        public AddSpeakerHandler(ISpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        public async Task<Null> HandleAsync(SpeakerDto dto)
        {
            await _speakerService.AddAsync(dto);
            return new Null();
        }
    }
}