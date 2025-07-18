namespace CheapGames.Dtos.Game
{
    public class FilterParamsDto
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 12;
        public string? Category { get; set; }
        public string? Platform { get; set; }
        public int minPrice { get; set; } = 0;
        public int maxPrice { get; set; } = 1000;

    }
}
