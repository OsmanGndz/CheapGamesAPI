using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Game>> GetFavoriesAsync(int userId);
        Task<bool> DeleteFavoriteAsync(int userId, int gameId);
        Task<bool> AddFavoriteAsync(int userId, int gameId);
    }
}
