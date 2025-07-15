using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.Game
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Game name is required.")]
        [MinLength(3, ErrorMessage = "Game name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Game name cannot exceed 100 characters.")]
        public string GameName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Game description is required.")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string GameDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Game image URL is required.")]
        [Url(ErrorMessage = "Game image must be a valid URL.")]
        public string GameImage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000.")]
        public decimal GamePrice { get; set; }

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public int GameDiscount { get; set; }

        [Required(ErrorMessage = "Total sales is required.")]
        [Range(0, 100000000, ErrorMessage = "Total sales must be between 0 and 100 million.")]
        public int TotalSales { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [MinLength(2, ErrorMessage = "Category name must be at least 2 characters.")]
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Platform name is required.")]
        [MinLength(2, ErrorMessage = "Platform name must be at least 2 characters.")]
        public string? PlatformName { get; set; }

        public bool isStanding { get; set; } = false;

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
    }
}
