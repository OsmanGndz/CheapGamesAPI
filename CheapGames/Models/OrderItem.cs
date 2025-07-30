using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;
namespace CheapGames.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int GameId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; } = null!;
        
        public Game Game { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtPurchase { get; set; }
    }
}
