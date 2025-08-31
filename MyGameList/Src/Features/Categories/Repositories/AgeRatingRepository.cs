using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IAgeRatingRepository : IGenericRepository<AgeRating, int>
    {
        Task<AgeRating?> IsDataDuplicateAsync(int? id, AgeRating compare);
    }

    public class AgeRatingRepository(MyGameListDbContext context) : GenericRepository<AgeRating, int>(context), IAgeRatingRepository
    {
        public async Task<AgeRating?> IsDataDuplicateAsync(int? id, AgeRating compare)
        {
            if (id.HasValue)
                return await context.AgeRating.FirstOrDefaultAsync(ageRating => ageRating.Id != id && ageRating.Rating == compare.Rating);
            else
                return await context.AgeRating.FirstOrDefaultAsync(ageRating => ageRating.Rating == compare.Rating);
        }
    }
}