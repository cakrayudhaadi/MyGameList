using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Characters.Repositories
{
    public interface ICharacterRepository
    {
        Task<Character> AddAsync(Character character);
        Task<List<Character>> GetAllCharactersAsync();
        Task<Character?> GetCharacterByIdAsync(int id);
        Task<Character?> GetCharacterByTitleAsync(int? id, string name);
        Task UpdateCharacterAsync(Character character);
        Task DeleteCharacterAsync(Character character);
        Task<List<Character>> GetCharacterListByIdsAsync(List<int> ids);
    }

    public class CharacterRepository(MyGameListDbContext context) : ICharacterRepository
    {
        public async Task<Character> AddAsync(Character character)
        {
            ArgumentNullException.ThrowIfNull(character);
            context.Character.Add(character);
            await context.SaveChangesAsync();
            return character;
        }

        public async Task<List<Character>> GetAllCharactersAsync()
        {
            return await context.Character
                .Include(character => character.CharacterRole)
                .Include(character => character.Games)
                .ToListAsync();
        }

        public async Task<Character?> GetCharacterByIdAsync(int id)
        {
            return await context.Character
                .Include(character => character.CharacterRole)
                .Include(character => character.Games)
                .FirstOrDefaultAsync(character => character.Id == id);
        }

        public async Task<Character?> GetCharacterByTitleAsync(int? id, string name)
        {
            if (id.HasValue)
                return await context.Character.FirstOrDefaultAsync(character => character.Id != id && character.Name == name);
            else
                return await context.Character.FirstOrDefaultAsync(character => character.Name == name);
        }

        public async Task UpdateCharacterAsync(Character character)
        {
            ArgumentNullException.ThrowIfNull(character);
            context.Character.Update(character);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCharacterAsync(Character character)
        {
            ArgumentNullException.ThrowIfNull(character);
            context.Character.Remove(character);
            await context.SaveChangesAsync();
        }

        public async Task<List<Character>> GetCharacterListByIdsAsync(List<int> ids)
        {
            return await context.Character
                .Where(character => ids.Contains(character.Id))
                .ToListAsync();
        }
    }
}
