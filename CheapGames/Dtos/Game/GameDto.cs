using CheapGames.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheapGames.Dtos.Game
{
    public class GameReadDto
    {
        public int Id { get; set; } 
        public string GameName { get; set; } = string.Empty;
        public string GameDescription { get; set; } = string.Empty;
        public string GameImage { get; set; } = string.Empty;
        public decimal GamePrice { get; set; }
        public int GameDiscount { get; set; }
        public int TotalSales { get; set; }
        public string? CategoryName { get; set; }
        public string? PlatformName { get; set; }
        public bool isStanding { get; set; } = false;
        public DateTime? ReleaseDate { get; set; }
    }

}
