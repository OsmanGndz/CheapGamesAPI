using CheapGames.Dtos.Product;
using CheapGames.Models;

namespace CheapGames.Mappers
{
    public static class ProductMapper
    {

        public static ProductReadDto ToProductReadDto(this OrderItem orderItem)
        {
            return new ProductReadDto
            {
                Id = orderItem.Id,
                Game = orderItem.Game.ToGameReadDto(),
                PriceAtPurchase = orderItem.PriceAtPurchase,
            };

        }
    }
}
