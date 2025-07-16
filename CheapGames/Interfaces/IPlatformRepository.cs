using CheapGames.Models;
using CheapGames.Dtos.Platform;

namespace CheapGames.Interfaces
{
    public interface IPlatformRepository
    {
        Task<List<Platform>> GetPlatformsAsync();
        Task<Platform> GetPlatformByIdAsync(int id);
        Task<Platform> CreatePlatformAsync(Platform platform);
        Task<Platform> UpdatePlatformAsync(int id, PlatformUpdateDto platform);
        Task<Platform> DeletePlatformAsync(int id);
    }
}
