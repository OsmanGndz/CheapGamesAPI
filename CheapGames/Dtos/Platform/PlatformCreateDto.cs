using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.Platform
{
    public class PlatformCreateDto
    {
        [Required(ErrorMessage = "Platform name is required!")]
        [MinLength(3, ErrorMessage = "platform name must be at least 3 characters!")]
        [MaxLength(50, ErrorMessage = "platform name must not exceed 50 characters!")]
        public string PlatformName { get; set; } = string.Empty;
    }
}
