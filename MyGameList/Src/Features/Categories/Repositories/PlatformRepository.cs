using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IPlatformRepository
    {
        Task<Platform> AddAsync(Platform platform);
        Task<List<Platform>> GetAllPlatformsAsync();
        Task<Platform?> GetPlatformByIdAsync(int id);
        Task<Platform?> GetPlatformByOptionAsync(string option);
        Task UpdatePlatformAsync(Platform platform);
        Task DeletePlatformAsync(Platform platform);
    }

    public class PlatformRepository(MyGameListDbContext context) : IPlatformRepository
    {
        public async Task<Platform> AddAsync(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);
            context.Platform.Add(platform);
            await context.SaveChangesAsync();
            return platform;
        }

        public async Task<List<Platform>> GetAllPlatformsAsync()
        {
            return await context.Platform.ToListAsync();
        }

        public async Task<Platform?> GetPlatformByIdAsync(int id)
        {
            return await context.Platform.FindAsync(id);
        }

        public async Task<Platform?> GetPlatformByOptionAsync(string option)
        {
            return await context.Platform.FirstOrDefaultAsync(g => g.Option == option);
        }

        public async Task UpdatePlatformAsync(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);
            context.Platform.Update(platform);
            await context.SaveChangesAsync();
        }

        public async Task DeletePlatformAsync(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);
            context.Platform.Remove(platform);
            await context.SaveChangesAsync();
        }
    }
}
