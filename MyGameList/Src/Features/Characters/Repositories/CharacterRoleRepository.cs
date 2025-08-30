using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Characters.Models;

namespace MyGameList.Src.Features.Characters.Repositories
{
    public interface ICharacterRoleRepository
    {
        Task<CharacterRole> AddAsync(CharacterRole characterRole);
        Task<List<CharacterRole>> GetAllCharacterRolesAsync();
        Task<CharacterRole?> GetCharacterRoleByIdAsync(int id);
        Task<CharacterRole?> GetCharacterRoleByRoleAsync(int? id, string role);
        Task UpdateCharacterRoleAsync(CharacterRole characterRole);
        Task DeleteCharacterRoleAsync(CharacterRole characterRole);
    }

    public class CharacterRoleRepository(MyGameListDbContext context) : ICharacterRoleRepository
    {
        public async Task<CharacterRole> AddAsync(CharacterRole characterRole)
        {
            ArgumentNullException.ThrowIfNull(characterRole);
            context.CharacterRole.Add(characterRole);
            await context.SaveChangesAsync();
            return characterRole;
        }

        public async Task<List<CharacterRole>> GetAllCharacterRolesAsync()
        {
            return await context.CharacterRole.ToListAsync();
        }

        public async Task<CharacterRole?> GetCharacterRoleByIdAsync(int id)
        {
            return await context.CharacterRole.FindAsync(id);
        }

        public async Task<CharacterRole?> GetCharacterRoleByRoleAsync(int? id, string role)
        {
            if (id.HasValue)
                return await context.CharacterRole.FirstOrDefaultAsync(characterRole => characterRole.Id != id && characterRole.Role == role);
            else
                return await context.CharacterRole.FirstOrDefaultAsync(characterRole => characterRole.Role == role);
        }

        public async Task UpdateCharacterRoleAsync(CharacterRole characterRole)
        {
            ArgumentNullException.ThrowIfNull(characterRole);
            context.CharacterRole.Update(characterRole);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCharacterRoleAsync(CharacterRole characterRole)
        {
            ArgumentNullException.ThrowIfNull(characterRole);
            context.CharacterRole.Remove(characterRole);
            await context.SaveChangesAsync();
        }
    }
}
