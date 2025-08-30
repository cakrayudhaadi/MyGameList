using MyGameList.Src.Features.Characters.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Characters.Dtos
{
    public class CharacterRoleDto
    {
        public CharacterRoleDto()
        {
            Role = string.Empty;
        }

        public CharacterRoleDto(string role, string? description)
        {
            Role = role;
            Description = description;
        }

        public string Role { get; set; }
        public string? Description { get; set; }

        public CharacterRole CharacterRoleDtoToModel(CharacterRole? characterRole, int? id)
        {
            characterRole ??= new CharacterRole();
            DateTime timeNow = DateTime.UtcNow;

            characterRole.Role = !string.IsNullOrEmpty(Role) ? Role : characterRole.Role;
            characterRole.Description = !string.IsNullOrEmpty(Description) ? Description : characterRole.Description;
            if (!id.HasValue)
            {
                characterRole.CreatedAt = timeNow;
                characterRole.UpdatedAt = timeNow;
            }
            else
            {
                characterRole.Id = id.Value;
                characterRole.UpdatedAt = timeNow;
            }

            return characterRole;
        }

        public string? CharacterRoleValidation()
        {
            if (string.IsNullOrEmpty(Role))
                return "Role is required";

            return null;
        }
    }
}
