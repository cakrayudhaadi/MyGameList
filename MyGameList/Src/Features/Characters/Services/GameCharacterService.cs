using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.Characters.Repositories;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Games.Repositories;

namespace MyGameList.Src.Features.Characters.Services
{
    public interface IGameCharacterService
    {
        Task<List<Game>> GetGameListByIds(List<int> ids);
        Task<List<Character>> GetCharacterListByIds(List<int> ids);
    }

    public class GameCharacterService(IGameRepository gameRepo,
        ICharacterRepository characterRepo) : IGameCharacterService
    {
        public async Task<List<Game>> GetGameListByIds(List<int> ids)
        {
            List<Game> games = await gameRepo.GetGameListByIdsAsync(ids);

            return games;
        }

        public async Task<List<Character>> GetCharacterListByIds(List<int> ids)
        {
            List<Character> characters = await characterRepo.GetCharacterListByIdsAsync(ids);

            return characters;
        }
    }
}
