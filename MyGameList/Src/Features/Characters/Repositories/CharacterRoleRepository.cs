using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Characters.Repositories
{
    public interface ICharacterRoleRepository : IGenericRepository<CharacterRole, int>
    {
        Task<CharacterRole?> IsDataDuplicateAsync(int? id, CharacterRole compare);
    }

    public class CharacterRoleRepository(MyGameListDbContext context) : GenericRepository<CharacterRole, int>(context), ICharacterRoleRepository
    {
        public async Task<CharacterRole?> IsDataDuplicateAsync(int? id, CharacterRole compare)
        {
            if (id.HasValue)
                return await context.CharacterRole.FirstOrDefaultAsync(characterRole => characterRole.Id != id && characterRole.Role == compare.Role);
            else
                return await context.CharacterRole.FirstOrDefaultAsync(characterRole => characterRole.Role == compare.Role);
        }
    }
}
