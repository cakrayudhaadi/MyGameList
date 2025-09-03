using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Services
{
    public interface IGenreService
    {
        Task<Response<GenreResponseDto>> AddGenreAsync(GenreDto genreDto);
        Task<Response<List<GenreResponseDto>>> GetAllGenresAsync();
        Task<Response<GenreResponseDto>> GetGenreByIdAsync(int id);
        Task<Response> UpdateGenreAsync(int id, GenreDto genreDto);
        Task<Response> DeleteGenreAsync(int id);
        Task<List<Genre>> GetGenreListByIds(List<int> ids);
    }

    public class GenreService(IGenreRepository genreRepo) : IGenreService
    {
        public async Task<Response<GenreResponseDto>> AddGenreAsync(GenreDto genreDto)
        {
            ArgumentNullException.ThrowIfNull(genreDto);

            string? errValidation = genreDto.GenreValidation();
            if (errValidation is not null)
                return new Response<GenreResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Genre genre = genreDto.GenreDtoToModel(null, null);
            Genre? duplicate = await genreRepo.IsDataDuplicateAsync(null, genre);
            if (duplicate is not null)
                return new Response<GenreResponseDto>(HttpStatusCode.BadRequest, "Genre must be unique.", null);

            Genre newGenre = await genreRepo.AddAsync(genre);
            GenreResponseDto genreResponseDto = GenreResponseDto.GenreModelToResponseDto(newGenre);

            return new Response<GenreResponseDto>(HttpStatusCode.OK, "Genre created successfully.", genreResponseDto);
        }

        public async Task<Response<List<GenreResponseDto>>> GetAllGenresAsync()
        {
            List<Genre> genres = await genreRepo.GetAllDatasAsync();
            List<GenreResponseDto> genreResponseDtos = [.. genres.Select(genre => GenreResponseDto.GenreModelToResponseDto(genre))];

            return new Response<List<GenreResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), genreResponseDtos);
        }

        public async Task<Response<GenreResponseDto>> GetGenreByIdAsync(int id)
        {
            Genre? genre = await genreRepo.GetDataByIdAsync(id);
            if (genre is null)
                return new Response<GenreResponseDto>(HttpStatusCode.NotFound, "Genre not found.", null);

            GenreResponseDto genreResponseDto = GenreResponseDto.GenreModelToResponseDto(genre);

            return new Response<GenreResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), genreResponseDto);
        }

        public async Task<Response> UpdateGenreAsync(int id, GenreDto genreDto)
        {
            Genre? existingGenre = await genreRepo.GetDataByIdAsync(id);
            if (existingGenre is null)
                return new Response(HttpStatusCode.NotFound, "Genre not found.");

            Genre updatedGenre = genreDto.GenreDtoToModel(existingGenre, id);
            Genre? duplicate = await genreRepo.IsDataDuplicateAsync(id, updatedGenre);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Genre must be unique.");

            await genreRepo.UpdateDataAsync(updatedGenre);

            return new Response(HttpStatusCode.OK, "Genre updated successfully.");
        }

        public async Task<Response> DeleteGenreAsync(int id)
        {
            Genre? existingGenre = await genreRepo.GetDataByIdAsync(id);
            if (existingGenre is null)
                return new Response(HttpStatusCode.NotFound, "Genre not found.");

            await genreRepo.DeleteDataAsync(existingGenre);

            return new Response(HttpStatusCode.OK, "Genre deleted successfully.");
        }

        public async Task<List<Genre>> GetGenreListByIds(List<int> ids)
        {
            List<Genre> genres = await genreRepo.GetDatasByIdsAsync(ids);

            return genres;
        }
    }
}
