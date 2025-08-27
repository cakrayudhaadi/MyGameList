using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;

namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IDeveloperRepository
    {
        Task<Developer> AddAsync(Developer developer);
        Task<List<Developer>> GetAllDevelopersAsync();
        Task<Developer?> GetDeveloperByIdAsync(int id);
        Task<Developer?> GetDeveloperByNameAsync(string name);
        Task UpdateDeveloperAsync(Developer developer);
        Task DeleteDeveloperAsync(Developer developer);
    }

    public class DeveloperRepository(MyGameListDbContext context) : IDeveloperRepository
    {
        public async Task<Developer> AddAsync(Developer developer)
        {
            ArgumentNullException.ThrowIfNull(developer);
            context.Developer.Add(developer);
            await context.SaveChangesAsync();
            return developer;
        }

        public async Task<List<Developer>> GetAllDevelopersAsync()
        {
            return await context.Developer.ToListAsync();
        }

        public async Task<Developer?> GetDeveloperByIdAsync(int id)
        {
            return await context.Developer.FindAsync(id);
        }

        public async Task<Developer?> GetDeveloperByNameAsync(string name)
        {
            return await context.Developer.FirstOrDefaultAsync(developer => developer.Name == name);
        }

        public async Task UpdateDeveloperAsync(Developer developer)
        {
            ArgumentNullException.ThrowIfNull(developer);
            context.Developer.Update(developer);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDeveloperAsync(Developer developer)
        {
            ArgumentNullException.ThrowIfNull(developer);
            context.Developer.Remove(developer);
            await context.SaveChangesAsync();
        }
    }
}
