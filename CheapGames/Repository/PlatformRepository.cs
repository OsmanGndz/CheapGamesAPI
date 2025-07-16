using CheapGames.Data;
using CheapGames.Dtos.Platform;
using CheapGames.Interfaces;
using CheapGames.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CheapGames.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDBContext _context;
        public PlatformRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Platform>> GetPlatformsAsync()
        {
            var platforms = await _context.Platforms.ToListAsync();

            if (platforms == null || !platforms.Any())
            {
                return null;
            }

            return platforms;
        }

        public async Task<Platform> GetPlatformByIdAsync(int id)
        {
            var existPlatform = await _context.Platforms
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existPlatform == null)
            {
                return null;
            }

            return existPlatform;
        }

        public async Task<Platform> CreatePlatformAsync(Platform platform)
        {
            var platf = new Platform
            {
                PlatformName = platform.PlatformName
            };

            await _context.Platforms.AddAsync(platf);
            await _context.SaveChangesAsync();

            return platf;
        }

        public async Task<Platform> UpdatePlatformAsync(int id, PlatformUpdateDto platform)
        {
            var existPlatform  = await _context.Platforms
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existPlatform == null)
            {
                return null;
            }
            existPlatform.PlatformName = platform.PlatformName;

            await _context.SaveChangesAsync();

            return existPlatform;

        }

        public Task<Platform> UpdatePlatformAsync(PlatformUpdateDto platform)
        {
            throw new NotImplementedException();
        }

        public async Task<Platform> DeletePlatformAsync(int id)
        {
            var existPlatform = await _context.Platforms
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existPlatform == null)
            {
                return null;
            }

            _context.Platforms.Remove(existPlatform);
            await _context.SaveChangesAsync();

            return existPlatform;
        }
    }
}
