using System.ComponentModel.DataAnnotations;

namespace CheapGames.Dtos.Auth
{
    public class ChangePasswordDto
    {
        public string password { get; set; } = null!;
        [Required(ErrorMessage ="New password is required")]
        [MinLength(6, ErrorMessage ="Password must be at least 6 characters")]
        public string newPassword { get; set; } = null!;
        [Required(ErrorMessage = "Password confirmation is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string passwordConfirmation { get; set; } = null!;
    }
}
