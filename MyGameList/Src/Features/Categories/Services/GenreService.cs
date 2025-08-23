using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Repositories;

namespace MyGameList.Src.Features.Categories.Services
{
    public interface IGenreService
    {
        Task<Genre> CreateGenreAsync(GenreDto genreDto);
    }

    public class GenreService(IGenreRepository genreRepository) : IGenreService
    {
        public async Task<Genre> CreateGenreAsync(GenreDto genreDto)
        {
            ArgumentNullException.ThrowIfNull(genreDto);

            if (string.IsNullOrEmpty(genreDto.Genre))
                throw new ArgumentException("Genre are required.");

            Genre newGenre = genreDto.GenreDtoToModel(null, null);

            return await genreRepository.AddAsync(newGenre);
        }
    }
}
