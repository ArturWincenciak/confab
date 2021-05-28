using System.Collections.Generic;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.DTO
{
    internal class HostDetailsDto : HostDto
    {
        public IEnumerable<ConferenceDto> Conferences { get; set; }
    }
}