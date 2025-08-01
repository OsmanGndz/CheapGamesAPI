using CheapGames.Data;
using CheapGames.Dtos.Game;
using CheapGames.Interfaces;
using CheapGames.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using CheapGames.Mappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(string? categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
            return category;
        }

        public async Task<List<Game>> GetFilteredData(string filter)
        {
            var data = _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .AsQueryable();

            switch (filter)
            {
                case "standings":
                    return await data.Where(g => g.isStanding).Take(12).ToListAsync();

                case "mostsales":
                    return await data.OrderByDescending(g => g.TotalSales).Take(12).ToListAsync();

                case "newadded":
                    return await data.OrderByDescending(g => g.CreatedOn).Take(12).ToListAsync();

                case "preorder":
                    return await data.Where(g => g.ReleaseDate > DateTime.Now).Take(12).ToListAsync();

                default:
                    return new List<Game>();

            }

        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .FirstOrDefaultAsync(g=> g.Id == id);
        }

        public List<GameReadDto> GetGamesByCategoryAsync(string categoryName)
        {
            var data =  _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .AsQueryable();

            var categoryDto = data.Select(g => g.ToGameReadDto()).ToList();
            var filteredData = categoryDto.Where(g => g.CategoryName == categoryName).Take(12).ToList();

            if(filteredData == null || filteredData.Count == 0)
            {
                return new List<GameReadDto>();
            }

            return filteredData;
        }

        public List<GameReadDto> GetGamesByFilterAsync(FilterParamsDto filter)
        {
            List<GameReadDto> filteredData;
            var data = _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .AsQueryable();

            var dataDto = data.Select(g => g.ToGameReadDto()).ToList();

            if (filter.Platform == null && filter.Category == null)
            {
                var priceFilterAll = dataDto.Where(g => g.GamePrice >= filter.minPrice && g.GamePrice <= filter.maxPrice).ToList();
                return priceFilterAll;
            }else if (filter.Platform == null)
            {
                filteredData = dataDto.Where(g => g.CategoryName == filter.Category).ToList();
                var priceFiltered = filteredData.Where(g => g.GamePrice >= filter.minPrice && g.GamePrice <= filter.maxPrice).ToList();
                return priceFiltered;

            }

            filteredData = dataDto.Where(g => g.CategoryName == filter.Category && g.PlatformName == filter.Platform).ToList();
            var priceFilteredData = filteredData.Where(g => g.GamePrice >= filter.minPrice && g.GamePrice <= filter.maxPrice).ToList();


            if (filteredData == null || filteredData.Count == 0)
            {
                return new List<GameReadDto>();
            }

            return priceFilteredData;

        }

        public async Task<List<GameReadDto>> GetGamesByPlatformAsync(string platformName)
        {
            var data = await _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .ToListAsync();
            var platformDto = data.Select(g => g.ToGameReadDto()).ToList();
            var filteredData = platformDto.Where(g => g.PlatformName == platformName).ToList();

            if (filteredData == null || filteredData.Count == 0)
            {
                return new List<GameReadDto>();
            }

            return filteredData;
        }

        public async Task<Platform?> GetPlatformAsync(string? platformName)
        {
            var platform = await _context.Platforms.FirstOrDefaultAsync(p => p.PlatformName == platformName);
            return platform;
        }

        public  PriceDto GetPriceRangeByFilterAsync(PriceRangeDto priceRangeInfo)
        {
            var data = _context.Games
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .AsQueryable();

            var dataDto = data.Select(g => g.ToGameReadDto()).ToList();
            var MinPrice = dataDto.Min(g => g.GamePrice);
            var MaxPrice = dataDto.Max(g => g.GamePrice);

            if (priceRangeInfo.discount && (priceRangeInfo.categoryName == null || priceRangeInfo.platformName == null))
            {
                dataDto = dataDto.Where(g=> g.GameDiscount > 0 ).ToList();

            }
            else
            {
                dataDto = dataDto.Where(g =>
                    g.CategoryName == priceRangeInfo.categoryName &&
                    (string.IsNullOrEmpty(priceRangeInfo.platformName) || g.PlatformName == priceRangeInfo.platformName)
                ).ToList();
            }

            if (dataDto == null || dataDto.Count == 0)
            {
                return new PriceDto { minPrice = 0, maxPrice = 0 };
            }
            MinPrice = dataDto.Min(g => g.GamePrice);
            MaxPrice = dataDto.Max(g => g.GamePrice);
            return new PriceDto { minPrice = MinPrice, maxPrice = MaxPrice };
        }

        public FilteredGameDto GetSearchedGamesAsync(string searchTerm)
        {
            string pattern = $"%{searchTerm}%";
            var query = _context.Games.Where(g =>
                 EF.Functions.Like(g.GameName, pattern) ||
                 EF.Functions.Like(g.GameDescription, pattern));

            var dataDto = query
                .Select(g => g.ToGameReadDto())
                .ToList();

            var dataAll = new FilteredGameDto
            {
                totalGame = dataDto.Count,
                games = dataDto
            };

            return dataAll;
        }

        public List<GameReadDto> GetSortedGamesAsync(List<GameReadDto> data, string sortingFilter)
        {
            List<GameReadDto> sortedData = sortingFilter.ToLower() switch
            {
                "default" => data,
                "price-asc" => data.OrderBy(g => g.GamePrice).ToList(),
                "price-desc" => data.OrderByDescending(g => g.GamePrice).ToList(),
                "name-asc" => data.OrderBy(g => g.GameName).ToList(),
                "name-desc" => data.OrderByDescending(g => g.GameName).ToList(),
                _ => data // Return unsorted data for unknown filter  
            };

            return sortedData;
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
            existGame.isStanding = game.isStanding;
            existGame.ReleaseDate = game.ReleaseDate;

            await _context.SaveChangesAsync();

            return existGame;
        }
    }
}
