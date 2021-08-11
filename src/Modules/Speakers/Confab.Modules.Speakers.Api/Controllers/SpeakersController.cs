using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers
{
    internal class SpeakersController : SpeakersControllerBase
    {
        private readonly ISpeakerService _service;

        public SpeakersController(ISpeakerService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SpeakerDto>> GetAsync(Guid id) =>
            OkOrNotFound(await _service.GetAsync(id));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SpeakerDto>>> BrowseAsync() =>
            Ok(await _service.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync(SpeakerDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction("Get", new {id = dto.Id}, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, SpeakerDto dto)
        {
            dto.Id = id;
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}