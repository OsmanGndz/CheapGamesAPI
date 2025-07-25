using CheapGames.Dtos.Order;
using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetOrdersAsync();
        Task<Order?> GetOrderAsync(int id);
        Task<Order?> CreateOrderAsync(OrderCreateDto orderDto, int userId);
    }
}
