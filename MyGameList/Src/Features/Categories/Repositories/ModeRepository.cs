using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IModeRepository
    {
        Task<Mode> AddAsync(Mode mode);
        Task<List<Mode>> GetAllModesAsync();
        Task<Mode?> GetModeByIdAsync(int id);
        Task<Mode?> GetModeByOptionAsync(int? id, string option);
        Task UpdateModeAsync(Mode mode);
        Task DeleteModeAsync(Mode mode);
        Task<List<Mode>> GetModeListByIdsAsync(List<int> ids);
    }

    public class ModeRepository(MyGameListDbContext context) : IModeRepository
    {
        public async Task<Mode> AddAsync(Mode mode)
        {
            ArgumentNullException.ThrowIfNull(mode);
            context.Mode.Add(mode);
            await context.SaveChangesAsync();
            return mode;
        }

        public async Task<List<Mode>> GetAllModesAsync()
        {
            return await context.Mode.ToListAsync();
        }

        public async Task<Mode?> GetModeByIdAsync(int id)
        {
            return await context.Mode.FindAsync(id);
        }

        public async Task<Mode?> GetModeByOptionAsync(int? id, string option)
        {
            if (id.HasValue)
                return await context.Mode.FirstOrDefaultAsync(mode => mode.Id != id && mode.Option == option);
            else
                return await context.Mode.FirstOrDefaultAsync(mode => mode.Option == option);
        }

        public async Task UpdateModeAsync(Mode mode)
        {
            ArgumentNullException.ThrowIfNull(mode);
            context.Mode.Update(mode);
            await context.SaveChangesAsync();
        }

        public async Task DeleteModeAsync(Mode mode)
        {
            ArgumentNullException.ThrowIfNull(mode);
            context.Mode.Remove(mode);
            await context.SaveChangesAsync();
        }

        public async Task<List<Mode>> GetModeListByIdsAsync(List<int> ids)
        {
            return await context.Mode
                .Where(mode => ids.Contains(mode.Id))
                .ToListAsync();
        }
    }
}