using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Games.Repositories
{
    public interface IGameRepository
    {
        Task<Game> AddAsync(Game game);
        Task<List<Game>> GetAllGamesAsync();
        Task<Game?> GetGameByIdAsync(int id);
        Task<Game?> GetGameByTitleAsync(string title);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(Game game);
    }

    public class GameRepository(MyGameListDbContext context) : IGameRepository
    {
        public async Task<Game> AddAsync(Game game)
        {
            ArgumentNullException.ThrowIfNull(game);
            context.Game.Add(game);
            await context.SaveChangesAsync();
            return game;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await context.Game
                .Include(game => game.Developers)
                .Include(game => game.Publishers)
                .Include(game => game.Producers)
                .Include(game => game.Genres)
                .Include(game => game.Modes)
                .Include(game => game.Platforms)
                .ToListAsync();
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await context.Game
                .Include(game => game.Developers)
                .Include(game => game.Publishers)
                .Include(game => game.Producers)
                .Include(game => game.Genres)
                .Include(game => game.Modes)
                .Include(game => game.Platforms)
                .FirstOrDefaultAsync(game => game.Id == id);
        }

        public async Task<Game?> GetGameByTitleAsync(string title)
        {
            return await context.Game.FirstOrDefaultAsync(game => game.Title == title);
        }

        public async Task UpdateGameAsync(Game game)
        {
            ArgumentNullException.ThrowIfNull(game);
            context.Game.Update(game);
            await context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(Game game)
        {
            ArgumentNullException.ThrowIfNull(game);
            context.Game.Remove(game);
            await context.SaveChangesAsync();
        }
    }
}
