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
            var previouslyOrderedGameIds = await _context.Orders
                .Where(o => o.UserId == userId)
                .SelectMany(o => o.OrderItems.Select(oi => oi.GameId))
                .ToListAsync();

            var alreadyOrderedGameIds = orderDto.GameIds.Intersect(previouslyOrderedGameIds).ToList();
            if (alreadyOrderedGameIds.Any())
            {
                return null;
            }

            var games = await _context.Games
                .Where(g => orderDto.GameIds.Contains(g.Id))
                .Include(g => g.GameCategory)
                .Include(g => g.GamePlatform)
                .ToListAsync();

            if (games.Count == 0)
                return null;

            if (games.Count != orderDto.GameIds.Count)
            {
                throw new Exception("Some games could not be found in the database.");
            }

            decimal CalculateDiscountedPrice(decimal price, decimal discount)
            {
                return price - (price * (discount / 100m));
            }

            var totalPrice = games.Sum(g => CalculateDiscountedPrice(g.GamePrice, g.GameDiscount));

            var order = new Order
            {
                UserId = userId,
                TotalPrice = totalPrice,
                OrderItems = new List<OrderItem>()
            };

            foreach (var game in games)
            {
                var discountedPrice = CalculateDiscountedPrice(game.GamePrice, game.GameDiscount);

                order.OrderItems.Add(new OrderItem
                {
                    GameId = game.Id,
                    Order = order,
                    PriceAtPurchase = discountedPrice
                });
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }


        public async Task<Order?> GetOrderAsync(int id, int userId)
        {
            var data = await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.OrderItems)
                        .ThenInclude(g => g.Game)
                            .ThenInclude(g => g.GameCategory)
                    .Include(o => o.OrderItems)
                        .ThenInclude(g => g.Game)
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
                .Include(o => o.OrderItems)
                    .ThenInclude(g => g.Game)
                        .ThenInclude(g=> g.GameCategory)
                .Include(o => o.OrderItems)
                    .ThenInclude(g => g.Game)
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
