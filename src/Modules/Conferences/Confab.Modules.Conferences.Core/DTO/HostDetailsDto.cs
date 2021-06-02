using System.Collections.Generic;

namespace Confab.Modules.Conferences.Core.DTO
{
    internal class HostDetailsDto : HostDto
    {
        public IEnumerable<ConferenceDto> Conferences { get; set; }
    }
}