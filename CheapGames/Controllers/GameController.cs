using CheapGames.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CheapGames.Models;
using CheapGames.Dtos.Game;
using CheapGames.Mappers;
using CheapGames.Interfaces;

namespace CheapGames.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepo;
        public GameController(IGameRepository gameRepo)
        {
            _gameRepo = gameRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameRepo.GetAllGamesAsync();

            if (games == null || games.Count == 0)
            {
                return NotFound("No games found.");
            }

            var dto = games.Select(g=> g.ToGameReadDto()).ToList();

            return Ok(dto);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGameById([FromRoute] int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _gameRepo.GetGameByIdAsync(id);

            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }

            var dto = game.ToGameReadDto();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (gameDto == null)
            {
                return BadRequest("Game data is null.");
            }

            var category = await _gameRepo.GetCategoryAsync(gameDto.CategoryName);
            var platform = await _gameRepo.GetPlatformAsync(gameDto.PlatformName);

            if (category == null || platform == null)
            {
                return BadRequest("Invalid category or platform");
            }

            var newGame = gameDto.ToGameCreateDto(category, platform);
            await _gameRepo.CreateGameAsync(newGame);
            
            return CreatedAtAction(nameof(GetGameById), new { id = newGame.Id }, gameDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateGame([FromRoute] int id, [FromBody] UpdateGameDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _gameRepo.GetCategoryAsync(updateDto.CategoryName);
            var platform = await _gameRepo.GetPlatformAsync(updateDto.PlatformName);

            if (category == null || platform == null)
            {
                return BadRequest("Invalid category or platform");
            }

            var game = await _gameRepo.UpdateGameAsync(id, updateDto, category, platform);

            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }

            return Ok(game?.ToGameReadDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _gameRepo.DeleteGameAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }

            return NoContent();

        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredData([FromQuery] string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return BadRequest("Filter cannot be empty.");
            }

            var filteredGames = await _gameRepo.GetFilteredData(filter.ToLower());

            if (filteredGames == null || filteredGames.Count == 0)
            {
                return NotFound("No games found matching the filter.");
            }
            var dto = filteredGames.Select(g => g.ToGameReadDto()).ToList();
            return Ok(dto);
        }

        [HttpGet("platform")]
        public async Task<IActionResult> GetGamesByPlatform([FromQuery] string? platformName)
        {
            if (string.IsNullOrEmpty(platformName))
            {
                return BadRequest("Platform name cannot be empty.");
            }

            var platform = await _gameRepo.GetGamesByPlatformAsync(platformName);

            return Ok(platform);
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetGamesByCategory([FromQuery] string? categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return BadRequest("Category name cannot be empty.");
            }

            var category = await _gameRepo.GetGamesByCategoryAsync(categoryName);

            return Ok(category);
        }

        [HttpGet("all-filter")]
        public async Task<IActionResult> GetGamesByAllFilter([FromQuery] FilterParamsDto filter )
        {
            var filteredGames = await _gameRepo.GetGamesByFilterAsync(filter);

            var paginatedGames = filteredGames
            .Skip((filter.page - 1) * filter.pageSize)
            .Take(filter.pageSize)
            .ToList();

            return Ok(paginatedGames);
        }


        [HttpGet("priceRange")]
        public async Task<IActionResult> GetPriceRange([FromQuery] PriceRangeDto priceRangeInfo)
        {
            var priceRange = await _gameRepo.GetPriceRangeByFilterAsync(priceRangeInfo);

            return Ok(priceRange);

        }

    }
}
