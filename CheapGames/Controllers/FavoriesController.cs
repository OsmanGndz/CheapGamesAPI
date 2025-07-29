using CheapGames.Interfaces;
using CheapGames.Mappers;
using Microsoft.AspNetCore.Authorization;
using CheapGames.Dtos.Game;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CheapGames.Controllers
{
    [Route("api/favorite")]
    [ApiController]
    [Authorize]
    public class FavoriesController : ControllerBase
    {
        private readonly IFavoriteRepository _favRepo;

        public FavoriesController(IFavoriteRepository favRepo) 
        {
            _favRepo = favRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFavoriesById([FromQuery] int page, [FromQuery] int pageSize)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var favories = await _favRepo.GetFavoriesAsync(userId);

            if (favories == null) 
            {
                return NotFound("There is no favorite for this user");
            }

            var totalGames = favories.Count();

            var favoriteDto = favories.Select(f => f.ToGameReadDto());

            var paginatedGames = favoriteDto
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var Data = new FilteredGameDto
            {
                totalGame = totalGames,
                games = paginatedGames
            };

            return Ok(Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFavoriteById([FromRoute] int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var favories = await _favRepo.GetFavoriteByIdAsync(userId, id);

            if (favories == false)
            {
                return NotFound("There is no favorite for this user");
            }


            return Ok(favories);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteGame([FromBody] int gameId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var favories = await _favRepo.AddFavoriteAsync(userId, gameId);

            if (favories == false)
            {
                return NotFound($"This game is already favorite for " +
                    $"this user or there is no game with this id: {gameId}");
            }

            return Ok("Added to favories");
        }

        [HttpDelete("{gameId:int}")]
        public async Task<IActionResult> RemoveFavoriteGame([FromRoute] int gameId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var favories = await _favRepo.DeleteFavoriteAsync(userId, gameId);

            if (favories == false)
            {
                return NotFound($"This game is not favorite for " +
                    $"this user or there is no game with this id: {gameId}");
            }

            return Ok("Removed from favories");
        }
    }
}
