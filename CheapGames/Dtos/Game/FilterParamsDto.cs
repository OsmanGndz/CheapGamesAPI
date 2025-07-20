using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.Game
{
    public class FilterParamsDto
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 12;
        [Required(ErrorMessage = "Category name must be entered!")]
        [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string? Category { get; set; }
        public string? Platform { get; set; }
        [Required(ErrorMessage ="minPrice must be entered!")]
        [Range(0, 5000, ErrorMessage = "minPrice must be between 0 and 5000.")]
        public decimal minPrice { get; set; } = 0;

        [Required(ErrorMessage = "maxPrice must be entered!")]
        [Range(0, 5000, ErrorMessage = "maxPrice must be between 0 and 5000.")]
        public decimal maxPrice { get; set; } = 1000;
        public string sortingFilter { get; set; } = "default"; 

    }
}
