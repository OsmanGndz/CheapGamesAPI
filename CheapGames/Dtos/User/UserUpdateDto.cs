using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.User
{
    public class UserUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email must be entered!")]
        [EmailAddress(ErrorMessage = "It must contain a valid email format.")]
        public string Email { get; set; } = null!;
    }
}
