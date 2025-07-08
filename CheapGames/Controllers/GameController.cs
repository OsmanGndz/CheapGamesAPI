using CheapGames.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CheapGames.Models;
using CheapGames.Dtos.Game;
using CheapGames.Mappers;

namespace CheapGames.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public GameController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _context.Games.
                Include(g => g.GameCategory).
                Include(g => g.GamePlatform).
                ToListAsync();

            if (games == null || games.Count == 0)
            {
                return NotFound("No games found.");
            }

            var dto = games.Select(g=> g.ToGameReadDto()).ToList();

            return Ok(dto);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById([FromRoute] int id) 
        {
            var game = await _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }

            var dto = game.ToGameReadDto();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest("Game data is null.");
            }
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
        }
    }
}
