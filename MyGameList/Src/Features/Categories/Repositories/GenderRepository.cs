using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IGenderRepository
    {
        Task<Gender> AddAsync(Gender gender);
        Task<List<Gender>> GetAllGendersAsync();
        Task<Gender?> GetGenderByIdAsync(int id);
        Task<Gender?> GetGenderByOptionAsync(string option);
        Task UpdateGenderAsync(Gender gender);
        Task DeleteGenderAsync(Gender gender);
    }

    public class GenderRepository(MyGameListDbContext context) : IGenderRepository
    {
        public async Task<Gender> AddAsync(Gender gender)
        {
            ArgumentNullException.ThrowIfNull(gender);
            context.Gender.Add(gender);
            await context.SaveChangesAsync();
            return gender;
        }

        public async Task<List<Gender>> GetAllGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<Gender?> GetGenderByIdAsync(int id)
        {
            return await context.Gender.FindAsync(id);
        }

        public async Task<Gender?> GetGenderByOptionAsync(string option)
        {
            return await context.Gender.FirstOrDefaultAsync(g => g.Option == option);
        }

        public async Task UpdateGenderAsync(Gender gender)
        {
            ArgumentNullException.ThrowIfNull(gender);
            context.Gender.Update(gender);
            await context.SaveChangesAsync();
        }

        public async Task DeleteGenderAsync(Gender gender)
        {
            ArgumentNullException.ThrowIfNull(gender);
            context.Gender.Remove(gender);
            await context.SaveChangesAsync();
        }
    }
}
