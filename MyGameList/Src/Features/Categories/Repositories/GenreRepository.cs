using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> AddAsync(Genre genre);
        Task<List<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
        Task<Genre?> GetGenreByOptionAsync(string option);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreAsync(Genre genre);
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

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await context.Genre.ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await context.Genre.FindAsync(id);
        }

        public async Task<Genre?> GetGenreByOptionAsync(string option)
        {
            return await context.Genre.FirstOrDefaultAsync(g => g.Option == option);
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            ArgumentNullException.ThrowIfNull(genre);
            context.Genre.Update(genre);
            await context.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(Genre genre)
        {
            ArgumentNullException.ThrowIfNull(genre);
            context.Genre.Remove(genre);
            await context.SaveChangesAsync();
        }
    }
}
