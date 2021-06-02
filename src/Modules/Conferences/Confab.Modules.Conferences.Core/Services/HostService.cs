using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class HostService : IHostService
    {
        private readonly IHostRepository _hostRepository;
        private readonly IHostDeletionPolice _hostDeletionPolice;

        public HostService(IHostRepository hostRepository, IHostDeletionPolice hostDeletionPolice)
        {
            _hostRepository = hostRepository;
            _hostDeletionPolice = hostDeletionPolice;
        }

        public async Task AddAsync(HostDto dto)
        {
            dto.Id = Guid.NewGuid();
            await _hostRepository.AddAsync(new Host
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            });
        }

        public async Task<HostDetailsDto> GetAsync(Guid id)
        {
            var host = await _hostRepository.GetAsync(id);
            if (host is null)
            {
                return null;
            }

            var dto = Map<HostDetailsDto>(host);

            dto.Conferences = host.Conferences.Select(c => new ConferenceDto
            {
                Id = c.Id,
                HostId = c.HostId,
                HostName = c.Host.Name,
                From = c.From,
                To = c.To,
                Name = c.Name,
                Localization = c.Localization,
                LogoUrl = c.LogoUrl,
                ParticipantsLimit = c.ParticipantsLimit
            }).ToList();
            
            return dto;
        }

        public async Task<IReadOnlyList<HostDto>> BrowseAsync()
        {
            var hosts = await _hostRepository.BrowseAsync();
            return hosts.Select(Map<HostDto>).ToList();
        }

        public async Task UpdateAsync(HostDetailsDto dto)
        {
            var host = await _hostRepository.GetAsync(dto.Id);
            if (host is null)
            {
                throw new HostNotFoundException(dto.Id);
            }

            host.Name = dto.Name;
            host.Description = dto.Description;

            await _hostRepository.UpdateAsync(host);
        }

        public async Task DeleteAsync(Guid id)
        {
            var host = await _hostRepository.GetAsync(id);
            if (host is null)
            {
                throw new HostNotFoundException(id);
            }

            if (await _hostDeletionPolice.CanDeleteAsync(host) is false)
            {
                throw new CannotDeleteHostException(host.Id);
            }

            await _hostRepository.DeleteAsync(host);
        }

        private static T Map<T>(Host host) where T : HostDto, new() =>
            new T
            {
                Id = host.Id,
                Name = host.Name,
                Description = host.Description
            };
    }
}