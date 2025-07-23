using CheapGames.Dtos.User;
using CheapGames.Models;

namespace CheapGames.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUsersByIdAsync(int id);
        string EncryptPassword(string password);
        Task<UserReadDto> Register(UserCreateDto userDto);
        Task<bool> IsUserExistByEmail(UserCreateDto userDto);
        Task<User?> UpdateUserAsync(int id, UserUpdateDto userDto);
        Task<bool> IsUserExistById(int id);
        Task<UserReadDto> DeleteUserAsync(int id);
    }
}
