using System.ComponentModel.DataAnnotations;
namespace CheapGames.Dtos.Category
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "CategoryName is required!")]
        [MinLength(3, ErrorMessage = "CategoryName must be at least 3 characters!")]
        [MaxLength(50, ErrorMessage = "CategoryName must not exceed 50 characters!")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
