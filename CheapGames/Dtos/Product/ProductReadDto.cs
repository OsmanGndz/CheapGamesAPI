using CheapGames.Dtos.Game;
using CheapGames.Models;

namespace CheapGames.Dtos.Product
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public GameReadDto Game { get; set; } = null!;
        public decimal PriceAtPurchase { get; set; }
    }
}
