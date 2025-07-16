using CheapGames.Dtos.Platform;
using CheapGames.Models;

namespace CheapGames.Mappers
{
    public static class PlatformMappers
    {
        public static PlatformReadDto ToPlatformReadDto(this Platform platform)
        {
            if (platform == null)
            {
                return null;
            }
            return new PlatformReadDto
            {
                Id = platform.Id,
                PlatformName = platform.PlatformName,
            };
        }

        public static Platform ToPlatformCreateDto(this PlatformCreateDto platform)
        {
            if (platform == null)
            {
                return null;
            }
            return new Platform
            {
                PlatformName = platform.PlatformName,
            };
        }

        public static Platform ToPlatformUpdateDto(this PlatformUpdateDto platform)
        {
            if (platform == null)
            {
                return null;
            }
            return new Platform
            {
                PlatformName = platform.PlatformName,
            };
        }
    }
}
