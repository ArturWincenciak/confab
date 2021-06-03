using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    internal class HostController : ConferencesControllerBase
    {
        private readonly IHostService _hostService;

        public HostController(IHostService hostService)
        {
            _hostService = hostService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<HostDetailsDto>> GetAsync(Guid id) =>
            OkOrNotFound(await _hostService.GetAsync(id));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HostDto>>> BrowseAsync() =>
            Ok(await _hostService.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync(HostDto dto)
        {
            await _hostService.AddAsync(dto);
            return CreatedAtAction("Get", new {id = dto.Id}, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, HostDetailsDto dto)
        {
            dto.Id = id;
            await _hostService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _hostService.DeleteAsync(id);
            return NoContent();
        }
    }
}