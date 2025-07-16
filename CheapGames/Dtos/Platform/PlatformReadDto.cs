using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.Platform
{
    public class PlatformReadDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Platform name is required!")]
        [MinLength(3, ErrorMessage = "Platform name must be at least 2 characters long!")]
        [MaxLength(50, ErrorMessage = "Platform name must be at most 50 characters long!")]
        public string PlatformName { get; set; } = string.Empty;
    }
}
