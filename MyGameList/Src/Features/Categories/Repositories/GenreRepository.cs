using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre, int>
    {
        Task<Genre?> IsDataDuplicateAsync(int? id, Genre compare);
    }

    public class GenreRepository(MyGameListDbContext context) : GenericRepository<Genre, int>(context), IGenreRepository
    {
        public async Task<Genre?> IsDataDuplicateAsync(int? id, Genre compare)
        {
            if (id.HasValue)
                return await context.Genre.FirstOrDefaultAsync(genre => genre.Id != id && genre.Option == compare.Option);
            else
                return await context.Genre.FirstOrDefaultAsync(genre => genre.Option == compare.Option);
        }
    }
}
