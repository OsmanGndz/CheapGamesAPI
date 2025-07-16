using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CheapGames.Interfaces;
using CheapGames.Mappers;
using CheapGames.Dtos.Platform;

namespace CheapGames.Controllers
{
    [Route("api/platform")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepo;
        public PlatformController(IPlatformRepository platformRepo)
        {
            _platformRepo = platformRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var platforms = await _platformRepo.GetPlatformsAsync();
            if (platforms == null || !platforms.Any())
            {
                return NotFound("No platforms found.");
            }
            var platformDto = platforms.Select(p => p.ToPlatformReadDto()).ToList();

            return Ok(platformDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPlatformById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var platform = await _platformRepo.GetPlatformByIdAsync(id);
            if (platform == null)
            {
                return NotFound($"Platform with ID {id} not found.");
            }
            var platformDto = platform.ToPlatformReadDto();
            return Ok(platformDto);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform([FromBody] PlatformCreateDto platformDto)
        {
            if (platformDto == null)
            {
                return BadRequest("Platform data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var platformD = platformDto.ToPlatformCreateDto();
            var newPlatform = await _platformRepo.CreatePlatformAsync(platformD);

            return CreatedAtAction(nameof(GetPlatformById), new { id = newPlatform.Id }, newPlatform.ToPlatformReadDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePlatform([FromRoute] int id, [FromBody] PlatformUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPlatform = await _platformRepo.UpdatePlatformAsync(id, updateDto);

            return Ok(updatedPlatform.ToPlatformReadDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePlatform([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletedPlatform = await _platformRepo.DeletePlatformAsync(id);
            if (deletedPlatform == null)
            {
                return NotFound($"Platform with ID {id} not found.");
            }
            return Ok(deletedPlatform.ToPlatformReadDto());
        }
    }
}
