using CheapGames.Dtos.Game;
using CheapGames.Dtos.Order;
using CheapGames.Models;

namespace CheapGames.Mappers
{
    public static class OrderMapper
    {
        public static OrderReadDto? ToOrderReadDto(this Order order)
        {
            if (order == null) return null;

            return new OrderReadDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                Games = order.OrderItems.Select(g => g.Game.ToGameReadDto()).ToList()
            };

        }
    }
}
