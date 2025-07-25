using CheapGames.Dtos.Game;

namespace CheapGames.Dtos.Order
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; } = 0;
        public List<GameReadDto>? Games { get; set; }
    }
}
