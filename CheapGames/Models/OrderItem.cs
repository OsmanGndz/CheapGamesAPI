using System.ComponentModel.DataAnnotations.Schema;

namespace CheapGames.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int GameId { get; set; }

        public Order Order { get; set; } = null!;
        public Game Game { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtPurchase { get; set; }
    }
}
