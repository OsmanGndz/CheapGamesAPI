using CheapGames.Data;
using CheapGames.Dtos.Game;
using CheapGames.Interfaces;
using CheapGames.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CheapGames.Repository

{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDBContext _context;
        public GameRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game?> DeleteGameAsync(int id)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return null;
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return game;

        }

        public Task<List<Game>> GetAllGamesAsync()
        {
            return _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(string? categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
            return category;
        }

        public Task<Game?> GetGameByIdAsync(int id)
        {
            return _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .FirstOrDefaultAsync(g=> g.Id == id);
        }

        public async Task<Platform?> GetPlatformAsync(string? platformName)
        {
            var platform = await _context.Platforms.FirstOrDefaultAsync(p => p.PlatformName == platformName);
            return platform;
        }

        public async Task<Game?> UpdateGameAsync(int id, UpdateGameDto game, Category category, Platform platform)
        {
            var existGame = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (existGame == null)
            {
                return null;
            }

            existGame.GameName = game.GameName;
            existGame.GameDescription = game.GameDescription;
            existGame.GamePrice = game.GamePrice;
            existGame.GameImage = game.GameImage;
            existGame.GameDiscount = game.GameDiscount;
            existGame.TotalSales = game.TotalSales;
            existGame.CategoryId = category.Id;
            existGame.PlatformId = platform.Id;

            await _context.SaveChangesAsync();

            return existGame;
        }
    }
}
