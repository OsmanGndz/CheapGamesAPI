namespace CheapGames.Dtos.Game
{
    public class UpdateGameDto
    {
        public string GameName { get; set; } = string.Empty;
        public string GameDescription { get; set; } = string.Empty;
        public string GameImage { get; set; } = string.Empty;
        public decimal GamePrice { get; set; }
        public int GameDiscount { get; set; }
        public int TotalSales { get; set; }
        public string? CategoryName { get; set; }
        public string? PlatformName { get; set; }
    }
}
