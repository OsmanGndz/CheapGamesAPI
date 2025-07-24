namespace CheapGames.Dtos.Auth
{
    public class ChangePasswordDto
    {
        public string password {  get; set; }
        public string newPassword { get; set; }
        public string passwordConfirmation { get; set; }
    }
}
