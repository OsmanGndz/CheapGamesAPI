using CheapGames.Data;
using CheapGames.Interfaces;
using CheapGames.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CheapGames.Repository
{
    public class FavoriteRepository:IFavoriteRepository
    {
        private readonly ApplicationDBContext _context;

        public FavoriteRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<bool> AddFavoriteAsync(int userId, int gameId)
        {
            var gameExist = await _context.Games.AnyAsync(g => g.Id == gameId);
            if (!gameExist) return false;

            var exists = await _context.FavoriteGames.AnyAsync(fg => fg.UserId == userId && fg.GameId == gameId);
            if (exists) return false;

            var favorite = new FavoriteGame { UserId = userId, GameId = gameId };
            _context.FavoriteGames.Add(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFavoriteAsync(int userId, int gameId)
        {
            var favorite = await _context.FavoriteGames.FirstOrDefaultAsync(fg => fg.UserId == userId && fg.GameId == gameId);
            if (favorite == null) return false;

            _context.FavoriteGames.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Game>> GetFavoriesAsync(int userId)
        {
            var favorites = await _context.FavoriteGames
                        .Where(fg => fg.UserId == userId)
                        .Include(fg => fg.Game)
                            .ThenInclude(g => g.GameCategory)
                        .Include(fg => fg.Game)
                            .ThenInclude(g => g.GamePlatform)
                        .Select(fg => fg.Game)
                        .ToListAsync();


            return favorites;
        }

        public async Task<bool> GetFavoriteByIdAsync(int userId, int gameId)
        {
            var isFavorite = await _context.FavoriteGames
                        .AnyAsync(fg => fg.UserId == userId && fg.GameId == gameId);

            return isFavorite;
        }
    }
}
