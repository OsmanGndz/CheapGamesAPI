using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.User
{
    public class UserCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Email must be entered!")]
        [EmailAddress(ErrorMessage = "It must contain a valid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
