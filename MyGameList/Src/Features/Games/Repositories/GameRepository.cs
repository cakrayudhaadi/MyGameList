using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Games.Repositories
{
    public interface IGameRepository : IGenericRepository<Game, int>
    {
        Task<Game?> IsDataDuplicateAsync(int? id, Game compare);
    }

    public class GameRepository(MyGameListDbContext context) : GenericRepository<Game, int>(context), IGameRepository
    {
        public async Task<List<Game>> GetAllDatasAsync()
        {
            return await context.Game
                .Include(game => game.AgeRatings)
                .Include(game => game.Developers)
                .Include(game => game.Publishers)
                .Include(game => game.Producers)
                .Include(game => game.Genres)
                .Include(game => game.Modes)
                .Include(game => game.Platforms)
                .ToListAsync();
        }

        public async Task<Game?> GetDataByIdAsync(int id)
        {
            return await context.Game
                .Include(game => game.AgeRatings)
                .Include(game => game.Developers)
                .Include(game => game.Publishers)
                .Include(game => game.Producers)
                .Include(game => game.Genres)
                .Include(game => game.Modes)
                .Include(game => game.Platforms)
                .FirstOrDefaultAsync(game => game.Id == id);
        }

        public async Task<Game?> IsDataDuplicateAsync(int? id, Game compare)
        {
            if (id.HasValue)
                return await context.Game.FirstOrDefaultAsync(game => game.Id != id && game.Title == compare.Title);
            else
                return await context.Game.FirstOrDefaultAsync(game => game.Title == compare.Title);
        }
    }
}
