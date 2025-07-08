namespace CheapGames.Mappers;
using CheapGames.Models;

    public static class GameMappers
    {
        public static Dtos.Game.GameReadDto ToGameReadDto(this Game game)
        {
            if (game == null) return null;
            return new Dtos.Game.GameReadDto
            {
                Id = game.Id,
                GameName = game.GameName,
                GameDescription = game.GameDescription,
                CreatedOn = game.CreatedOn,
                GameImage = game.GameImage,
                GamePrice = game.GamePrice,
                GameDiscount = game.GameDiscount,
                TotalSales = game.TotalSales,
                CategoryName = game.GameCategory?.CategoryName,
                PlatformName = game.GamePlatform?.PlatformName
            };
        }

    }

