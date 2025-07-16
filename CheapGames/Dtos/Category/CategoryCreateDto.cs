using System.ComponentModel.DataAnnotations;
namespace CheapGames.Dtos.Category
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage ="Category Name is required!")]
        [MinLength(3, ErrorMessage = "Category Name must be at least 3 characters!")]
        [MaxLength(50, ErrorMessage = "Category Name cannot be over 50!")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
