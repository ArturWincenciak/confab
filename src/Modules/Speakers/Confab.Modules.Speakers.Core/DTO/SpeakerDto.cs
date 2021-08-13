using System;
using System.ComponentModel.DataAnnotations;

namespace Confab.Modules.Speakers.Core.DTO
{
    internal class SpeakerDto
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Bio { get; set; }

        public string AvatarUrl { get; set; }
    }
}
