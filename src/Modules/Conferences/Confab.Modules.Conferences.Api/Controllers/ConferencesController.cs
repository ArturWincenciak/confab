using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Authorize(Policy = Policy)]
    internal class ConferencesController : ConferencesControllerBase
    {
        public const string Policy = "conferences";

        private readonly IConferenceService _conferenceService;

        public ConferencesController(IConferenceService conferenceService)
        {
            _conferenceService = conferenceService;
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConferenceDetailsDto>> GetAsync(Guid id) =>
            OkOrNotFound(await _conferenceService.GetAsync(id));

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> BrowseAsync() =>
            Ok(await _conferenceService.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync(ConferenceDetailsDto dto)
        {
            await _conferenceService.AddAsync(dto);
            return CreatedAtAction("Get", new {id = dto.Id}, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, ConferenceDetailsDto dto)
        {
            dto.Id = id;
            await _conferenceService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _conferenceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
