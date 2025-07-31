using CheapGames.Data;
using CheapGames.Dtos.Product;
using CheapGames.Interfaces;
using CheapGames.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CheapGames.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<List<int>> GetMyProductIdsAsync(int userId)
        {
            var previouslyOrderedGameIds = await _context.Orders
                .Where(o => o.UserId == userId)
                .SelectMany(o => o.OrderItems.Select(oi => oi.GameId))
                .ToListAsync();


            return previouslyOrderedGameIds;
        }

        public async Task<List<ProductReadDto>> GetOrderItems(int userId)
        {
            var orders = await _context.Orders
                .Where(o=> o.UserId == userId)
                .Include(o=> o.OrderItems)
                    .ThenInclude(ot=> ot.Game)
                        .ThenInclude(g=> g.GameCategory)
                .Include(o => o.OrderItems)
                    .ThenInclude(ot => ot.Game)
                        .ThenInclude(g => g.GamePlatform)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            if(orders == null || orders.Count == 0)
            {
                return new List<ProductReadDto>();
            }

            var products = orders.SelectMany(o=> o.OrderItems).ToList();
            var productsDto = products.Select(p => p.ToProductReadDto()).ToList();

            return productsDto;

        }
    }
}
