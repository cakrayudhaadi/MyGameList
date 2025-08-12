using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Game_List.DTOs;
using MyGameList.Data;
using MyGameList.Models;

namespace My_Game_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(MyGameListDbContext context) : ControllerBase
    {
        private readonly MyGameListDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Games>>> GetGames()
        {
            return Ok(await _context.Games.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> GetVideoGameById(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<Games>> AddGame(GamesDTO gameDTO)
        {
            if (gameDTO is null)
                return BadRequest();

            Games newGame = gameDTO.GamesDTOToModel(null, null);
            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, GamesDTO gameDTO)
        {
            var game = await _context.Games.FindAsync(id);
            if (game is null)
                return NotFound();

            game = gameDTO.GamesDTOToModel(game, id);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game is null)
                return NotFound();

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
