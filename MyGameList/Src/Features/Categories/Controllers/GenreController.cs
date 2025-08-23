using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Services;

namespace MyGameList.Src.Features.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController(IGenreService genreService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Genre>> AddGenre(GenreDto genreDto)
        {
            try
            {
                Genre newGenre = await genreService.CreateGenreAsync(genreDto);

                return CreatedAtAction(nameof(AddGenre), new { id = newGenre.Id }, newGenre);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
