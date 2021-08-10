using System;

namespace Confab.Modules.Speakers.Core.DTO
{
    internal class SpeakerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
    }
}
