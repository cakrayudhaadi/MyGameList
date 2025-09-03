using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Characters.Repositories
{
    public interface ICharacterRepository : IGenericRepository<Character, int>
    {
        Task<Character?> IsDataDuplicateAsync(int? id, Character compare);
    }

    public class CharacterRepository(MyGameListDbContext context) : GenericRepository<Character, int>(context), ICharacterRepository
    {
        public override async Task<List<Character>> GetAllDatasAsync()
        {
            return await context.Character
                .Include(character => character.CharacterRole)
                .Include(character => character.Games)
                .ToListAsync();
        }

        public override async Task<Character?> GetDataByIdAsync(int id)
        {
            return await context.Character
                .Include(character => character.CharacterRole)
                .Include(character => character.Games)
                .FirstOrDefaultAsync(character => character.Id == id);
        }

        public async Task<Character?> IsDataDuplicateAsync(int? id, Character compare)
        {
            if (id.HasValue)
                return await context.Character.FirstOrDefaultAsync(character => character.Id != id && character.Name == compare.Name);
            else
                return await context.Character.FirstOrDefaultAsync(character => character.Name == compare.Name);
        }
    }
}
