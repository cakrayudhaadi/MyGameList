using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Games.Dtos;
using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(MyGameListDbContext context) : ControllerBase
    {
        private readonly MyGameListDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Game>>> GetGames()
        {
            return Ok(await _context.Game.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetVideoGameById(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> AddGame(GameDto gameDto)
        {
            if (gameDto is null)
                return BadRequest();

            Game newGame = gameDto.GamesDtoToModel(null, null);
            _context.Game.Add(newGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, GameDto gameDto)
        {
            var game = await _context.Game.FindAsync(id);
            if (game is null)
                return NotFound();

            game = gameDto.GamesDtoToModel(game, id);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game is null)
                return NotFound();

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
