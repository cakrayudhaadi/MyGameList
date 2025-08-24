using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IAgeRatingRepository
    {
        Task<AgeRating> AddAsync(AgeRating ageRating);
        Task<List<AgeRating>> GetAllAgeRatingsAsync();
        Task<AgeRating?> GetAgeRatingByIdAsync(int id);
        Task<AgeRating?> GetAgeRatingByRatingAsync(string rating);
        Task UpdateAgeRatingAsync(AgeRating ageRating);
        Task DeleteAgeRatingAsync(AgeRating ageRating);
    }

    public class AgeRatingRepository(MyGameListDbContext context) : IAgeRatingRepository
    {
        public async Task<AgeRating> AddAsync(AgeRating ageRating)
        {
            ArgumentNullException.ThrowIfNull(ageRating);
            context.AgeRating.Add(ageRating);
            await context.SaveChangesAsync();
            return ageRating;
        }

        public async Task<List<AgeRating>> GetAllAgeRatingsAsync()
        {
            return await context.AgeRating.ToListAsync();
        }

        public async Task<AgeRating?> GetAgeRatingByIdAsync(int id)
        {
            return await context.AgeRating.FindAsync(id);
        }

        public async Task<AgeRating?> GetAgeRatingByRatingAsync(string rating)
        {
            return await context.AgeRating.FirstOrDefaultAsync(g => g.Rating == rating);
        }

        public async Task UpdateAgeRatingAsync(AgeRating ageRating)
        {
            ArgumentNullException.ThrowIfNull(ageRating);
            context.AgeRating.Update(ageRating);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAgeRatingAsync(AgeRating ageRating)
        {
            ArgumentNullException.ThrowIfNull(ageRating);
            context.AgeRating.Remove(ageRating);
            await context.SaveChangesAsync();
        }
    }
}