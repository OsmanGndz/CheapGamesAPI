using CheapGames.Dtos.Order;
using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetOrdersAsync(int userId);
        Task<Order?> GetOrderAsync(int id, int userId);
        Task<Order?> CreateOrderAsync(OrderCreateDto orderDto, int userId);
    }
}
