using System.ComponentModel.DataAnnotations.Schema;

namespace CheapGames.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } = 0;
        public List<OrderItem> OrderItems { get; set; } = new ();
        public User User { get; set; } = null!;

    }

}
