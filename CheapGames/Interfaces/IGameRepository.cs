using CheapGames.Dtos.Game;
using CheapGames.Models;
namespace CheapGames.Interfaces
{
    public interface IGameRepository
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game?> GetGameByIdAsync(int id);
        Task<Category?> GetCategoryAsync(string? categoryName);
        Task<Platform?> GetPlatformAsync(string? platformName);
        Task<Game> CreateGameAsync(Game game);
        Task<Game?> UpdateGameAsync(int id, UpdateGameDto game, Category category, Platform platform);
        Task<Game?> DeleteGameAsync(int id);
        Task<List<Game>> GetFilteredData(string filter);
        Task<List<GameReadDto>> GetGamesByPlatformAsync(string platformName);
        List<GameReadDto> GetGamesByCategoryAsync(string categoryName);

        List<GameReadDto> GetGamesByFilterAsync(FilterParamsDto filter);
        PriceDto GetPriceRangeByFilterAsync(PriceRangeDto priceRangeInfo);
        List<GameReadDto> GetSortedGamesAsync(List<GameReadDto> data, string sortingFilter);
        Task<FilteredGameDto> GetSearchedGamesAsync(string searchTerm);

    }
}
