using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> AuthenticateUserAsync(string email, string password);

    }
}
