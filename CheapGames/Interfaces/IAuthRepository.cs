using CheapGames.Dtos.Auth;
using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<User?> ChangePasswordAsync(int userId, ChangePasswordDto passwordDto);

    }
}
