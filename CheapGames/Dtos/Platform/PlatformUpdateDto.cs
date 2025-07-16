using System.ComponentModel.DataAnnotations;
namespace CheapGames.Dtos.Platform
{
    public class PlatformUpdateDto
    {
        [Required(ErrorMessage = "Platform name is required!")]
        [MinLength(3, ErrorMessage = "Platform name must be at least 3 characters long!")]
        [MaxLength(50, ErrorMessage = "Platform name must be at most 50 characters long!")]
        public string PlatformName { get; set; } = string.Empty;

    }
}
