using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mappings;
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

            var allWithTheEmail = await _repository.GetAsNoTrackingAsync(dto.Email);
            if (allWithTheEmail.Any())
                throw new SpeakerAlreadyExistsException(dto.Email);

            await _repository.AddAsync(dto.AsEntity());
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                throw new SpeakerNotFoundException(id);

            return entity.AsDto();
        }

        public async Task<IReadOnlyList<SpeakerDto>> BrowseAsync()
        {
            var entities = await _repository.BrowseAsync();
            return entities
                .Select(x => x.AsDto())
                .ToList();
        }

        public async Task UpdateAsync(SpeakerDto dto)
        {
            if (await _repository.ExistsAsync(dto.Id) is false)
                throw new SpeakerNotFoundException(dto.Id);

            var allWithTheEmail = await _repository.GetAsNoTrackingAsync(dto.Email);
            if (allWithTheEmail.Any(x => x.Id != dto.Id))
                throw new SpeakerAlreadyExistsException(dto.Email);

            await _repository.UpdateAsync(dto.AsEntity());
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
                throw new SpeakerNotFoundException(id);

            await _repository.DeleteAsync(entity);
        }
    }
}