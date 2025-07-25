using CheapGames.Data;
using CheapGames.Dtos.Order;
using CheapGames.Interfaces;
using CheapGames.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CheapGames.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Order?> CreateOrderAsync(OrderCreateDto orderDto, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null;

            var games = await _context.Games
                .Where(g => orderDto.GameIds.Contains(g.Id))
                .Include(g=> g.GameCategory)
                .Include(g=> g.GamePlatform)
                .ToListAsync();

            if (games.Count == 0)
                return null;

            var totalPrice = games.Sum(g => g.GamePrice);

            var order = new Order
            {
                UserId = userId,
                Games = games,
                TotalPrice = totalPrice,
            };


            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }


        public async Task<Order?> GetOrderAsync(int id)
        {
            var data = await _context.Orders.
                     Include(o => o.Games)
                    .ThenInclude(g => g.GameCategory)
                    .Include(o => o.Games)
                    .ThenInclude(g => g.GamePlatform)
                    .FirstOrDefaultAsync(o => o.Id == id);

            if (data == null)
            {
                return null;
            }

            return data;
        }

        public async Task<List<Order>?> GetOrdersAsync()
        {
            var data = await _context.Orders
        .Include(o => o.Games)
            .ThenInclude(g => g.GameCategory)
        .Include(o => o.Games)
            .ThenInclude(g => g.GamePlatform)
        .ToListAsync();

            if (data == null) 
            {
                return null;
            }

            return data;
        }
    }
}
