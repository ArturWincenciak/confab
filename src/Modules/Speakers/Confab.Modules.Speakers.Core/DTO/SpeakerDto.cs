using System;
using System.ComponentModel.DataAnnotations;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Speakers.Core.DTO
{
    internal class SpeakerDto : IRequestMessage<Null>, IModuleRequest
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Bio { get; set; }

        public string AvatarUrl { get; set; }
    }
}