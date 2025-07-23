using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface ITokenRepository
    {
        string CreateToken(User user);
    }
}
