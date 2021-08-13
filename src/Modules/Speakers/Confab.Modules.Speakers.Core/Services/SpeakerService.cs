using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Repositories;

namespace Confab.Modules.Speakers.Core.Services
{
    internal class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository _repository;

        public SpeakerService(ISpeakerRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(SpeakerDto dto)
        {
            dto.Id = Guid.NewGuid();

            var entity = await _repository.GetAsync(dto.Email);
            if (entity is not null)
            {
                throw new SpeakerAlreadyExistsException(dto.Email);
            }

            await _repository.AddAsync(new Speaker
            {
                Id = dto.Id,
                Email = dto.Email,
                FullName = dto.FullName,
                Bio = dto.Bio,
                AvatarUrl = dto.AvatarUrl
            });
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new SpeakerNotFoundException(id);
            }

            return new SpeakerDto
            {
                Id = entity.Id,
                Email = entity.Email,
                FullName = entity.FullName,
                Bio = entity.Bio,
                AvatarUrl = entity.AvatarUrl
            };
        }

        public async Task<IReadOnlyList<SpeakerDto>> BrowseAsync()
        {
            var entities = await _repository.BrowseAsync();
            return entities
                .Select(x => new SpeakerDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                    Bio = x.Bio,
                    AvatarUrl = x.AvatarUrl
                })
                .ToList();
        }

        public async Task UpdateAsync(SpeakerDto dto)
        {
            var entity = await _repository.GetAsync(dto.Id);
            if (entity is null)
            {
                throw new SpeakerNotFoundException(dto.Id);
            }

            if (await _repository.GetAsync(dto.Email) is not null)
            {
                throw new SpeakerAlreadyExistsException(dto.Email);
            }

            entity.FullName = dto.FullName;
            entity.Email = dto.Email;
            entity.Bio = dto.Bio;
            entity.AvatarUrl = dto.AvatarUrl;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
            {
                throw new SpeakerNotFoundException(id);
            }

            await _repository.DeleteAsync(entity);
        }
    }
}
