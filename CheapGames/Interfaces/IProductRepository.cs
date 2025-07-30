using CheapGames.Dtos.Product;
using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductReadDto>> GetOrderItems(int userId);
    }
}
