using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.Game
{
    public class FilterParamsDto
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 12;
        public string? Category { get; set; }
        public string? Platform { get; set; }
        [Required(ErrorMessage ="minPrice must be entered!")]
        [Range(0, 5000, ErrorMessage = "minPrice must be between 0 and 5000.")]
        public decimal minPrice { get; set; } = 0;

        [Required(ErrorMessage = "maxPrice must be entered!")]
        [Range(0, 5000, ErrorMessage = "maxPrice must be between 0 and 5000.")]
        public decimal maxPrice { get; set; } = 1000;
        public string sortingFilter { get; set; } = "default"; 
        public bool discount { get; set; } = false;

    }
}
