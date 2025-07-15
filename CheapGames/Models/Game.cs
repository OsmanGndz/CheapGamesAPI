using System.ComponentModel.DataAnnotations.Schema;

namespace CheapGames.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string GameName { get; set; } = string.Empty;
        public string GameDescription { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string GameImage { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal GamePrice { get; set; } = 0;
        public int GameDiscount { get; set; } = 0;
        public int? CategoryId { get; set; }
        public Category? GameCategory { get; set; }
        public int TotalSales { get; set; } = 0;
        public int? PlatformId { get; set; }
        public Platform? GamePlatform { get; set; }
        public bool isStanding { get; set; } = false;
        public DateTime? ReleaseDate { get; set; } 

    }
}
