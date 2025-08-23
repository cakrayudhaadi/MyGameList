using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> AddAsync(Genre genre);
    }

    public class GenreRepository(MyGameListDbContext context) : IGenreRepository
    {
        public async Task<Genre> AddAsync(Genre genre)
        {
            ArgumentNullException.ThrowIfNull(genre);
            context.Genre.Add(genre);
            await context.SaveChangesAsync();
            return genre;
        }
    }
}
