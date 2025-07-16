namespace CheapGames.Mappers;
using CheapGames.Models;
using CheapGames.Dtos.Game;

public static class GameMappers
    {
    public static GameReadDto ToGameReadDto(this Game game)
        {
            if (game == null) return null;
            return new GameReadDto
            {
                Id = game.Id,
                GameName = game.GameName,
                GameDescription = game.GameDescription,
                GameImage = game.GameImage,
                GamePrice = game.GamePrice,
                GameDiscount = game.GameDiscount,
                TotalSales = game.TotalSales,
                CategoryName = game.GameCategory?.CategoryName,
                PlatformName = game.GamePlatform?.PlatformName,
                isStanding = game.isStanding,
                ReleaseDate = game.ReleaseDate
            };
        }

        public static Game ToGameCreateDto(this CreateGameDto game, Category category, Platform platform)
        {
        
        return new Game
            {
                GameName = game.GameName,
                GameDescription = game.GameDescription,
                GameImage = game.GameImage,
                GamePrice = game.GamePrice,
                GameDiscount = game.GameDiscount,
                TotalSales = game.TotalSales,
                CategoryId = category.Id,
                PlatformId = platform.Id,
                isStanding = game.isStanding,
                ReleaseDate = game.ReleaseDate
        };

        }

    }

