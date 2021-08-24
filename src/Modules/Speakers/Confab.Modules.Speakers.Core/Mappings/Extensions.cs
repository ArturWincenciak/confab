using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;

namespace Confab.Modules.Speakers.Core.Mappings
{
    internal static class Extensions
    {
        public static Speaker AsEntity(this SpeakerDto dto)
        {
            return new Speaker
            {
                Id = dto.Id,
                Email = dto.Email,
                FullName = dto.FullName,
                Bio = dto.Bio,
                AvatarUrl = dto.AvatarUrl
            };
        }

        public static SpeakerDto AsDto(this Speaker entity)
        {
            return new SpeakerDto
            {
                Id = entity.Id,
                Email = entity.Email,
                FullName = entity.FullName,
                Bio = entity.Bio,
                AvatarUrl = entity.AvatarUrl
            };
        }
    }
}