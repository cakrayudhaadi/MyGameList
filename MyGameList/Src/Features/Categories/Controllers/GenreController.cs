using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController(IGenreService genreService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<GenreResponseDto>>> AddGenre(GenreDto genreDto)
        {
            try
            {
                return res.Result<GenreResponseDto>(await genreService.AddGenreAsync(genreDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<GenreResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GenreResponseDto>>>> GetAllGenres()
        {
            try
            {
                return res.Result<List<GenreResponseDto>>(await genreService.GetAllGenresAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<GenreResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GenreResponseDto>>> GetGenreById(int id)
        {
            try
            {
                return res.Result<GenreResponseDto>(await genreService.GetGenreByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<GenreResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateGenre(int id, GenreDto genreDto)
        {
            try
            {
                return res.Result(await genreService.UpdateGenreAsync(id, genreDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteGenre(int id)
        {
            try
            {
                return res.Result(await genreService.DeleteGenreAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
