using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email must be entered!!!")]
        [EmailAddress(ErrorMessage = "Email address must be valid format!!!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password must be entered!!!")]
        public string Password { get; set; } = null!;
    }
}
