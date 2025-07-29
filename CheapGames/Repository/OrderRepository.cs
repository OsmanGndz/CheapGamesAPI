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

            if(games.Count != orderDto.GameIds.Count)
            {
                throw new Exception("Some games could not found at database.");
            }

            var totalPrice = games.Sum(g => g.GamePrice - (g.GamePrice * (g.GameDiscount / 100)));

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


        public async Task<Order?> GetOrderAsync(int id, int userId)
        {
            var data = await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.Games)
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

        public async Task<List<Order>?> GetOrdersAsync(int userId)
        {
            var data = await _context.Orders
                .Where(o=> o.UserId == userId)
                .Include(o => o.Games)
                    .ThenInclude(g => g.GameCategory)
                .Include(o => o.Games)
                    .ThenInclude(g => g.GamePlatform)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            if (data == null) 
            {
                return null;
            }

            return data;
        }
    }
}
