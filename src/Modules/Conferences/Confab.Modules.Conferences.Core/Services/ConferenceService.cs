using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Shared.Abstractions.Events;
using ConferenceCreated = Confab.Modules.Conferences.Core.Events.ConferenceCreated;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class ConferenceService : IConferenceService
    {
        private readonly IConferenceDeletionPolice _conferenceDeletionPolice;
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHostRepository _hostRepository;

        public ConferenceService(IConferenceRepository conferenceRepository, IHostRepository hostRepository,
            IConferenceDeletionPolice conferenceDeletionPolice, IEventDispatcher eventDispatcher)
        {
            _conferenceRepository = conferenceRepository;
            _hostRepository = hostRepository;
            _conferenceDeletionPolice = conferenceDeletionPolice;
            _eventDispatcher = eventDispatcher;
        }

        public async Task AddAsync(ConferenceDetailsDto dto)
        {
            if (await _hostRepository.GetAsync(dto.HostId) is null)
                throw new HostNotFoundException(dto.HostId);

            dto.Id = Guid.NewGuid();
            var conference = new Conference
            {
                Id = dto.Id,
                HostId = dto.HostId,
                Name = dto.Name,
                Description = dto.Description,
                From = dto.From,
                To = dto.To,
                Localization = dto.Localization,
                LogoUrl = dto.LogoUrl,
                ParticipantsLimit = dto.ParticipantsLimit
            };
            await _conferenceRepository.AddAsync(conference);
            await _eventDispatcher.PublishAsync(new ConferenceCreated(conference.Id, conference.Name,
                conference.ParticipantsLimit,
                conference.From, conference.To));
        }

        public async Task<ConferenceDetailsDto> GetAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null)
                return null;

            var dto = Map<ConferenceDetailsDto>(conference);
            dto.Description = conference.Description;

            return dto;
        }

        public async Task<IReadOnlyList<ConferenceDto>> BrowseAsync()
        {
            var conferences = await _conferenceRepository.BrowseAsync();
            return conferences.Select(Map<ConferenceDto>).ToList();
        }

        public async Task UpdateAsync(ConferenceDetailsDto dto)
        {
            var conference = await _conferenceRepository.GetAsync(dto.Id);
            if (conference is null)
                throw new ConferenceNotFoundException(dto.Id);

            conference.Name = dto.Name;
            conference.Description = dto.Description;
            conference.Localization = dto.Localization;
            conference.LogoUrl = dto.LogoUrl;
            conference.From = dto.From;
            conference.To = dto.To;
            conference.ParticipantsLimit = dto.ParticipantsLimit;

            await _conferenceRepository.UpdateAsync(conference);
        }

        public async Task DeleteAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null)
                throw new ConferenceNotFoundException(id);

            if (await _conferenceDeletionPolice.CanDeleteAsync(conference) is false)
                throw new CannotDeleteConferenceException(id);

            await _conferenceRepository.DeleteAsync(conference);
        }

        private static T Map<T>(Conference entity) where T : ConferenceDto, new()
        {
            return new T
            {
                Id = entity.Id,
                HostId = entity.HostId,
                HostName = entity.Host?.Name,
                Name = entity.Name,
                Localization = entity.Localization,
                From = entity.From,
                To = entity.To,
                LogoUrl = entity.LogoUrl,
                ParticipantsLimit = entity.ParticipantsLimit
            };
        }
    }
}