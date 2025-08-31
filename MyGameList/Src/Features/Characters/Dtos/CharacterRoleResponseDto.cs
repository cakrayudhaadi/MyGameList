using MyGameList.Src.Features.Characters.Models;

namespace MyGameList.Src.Features.Characters.Dtos
{
    public class CharacterRoleResponseDto
    {
        public CharacterRoleResponseDto()
        {
            Role = string.Empty;
        }

        public CharacterRoleResponseDto(int id, string role, string? description)
        {
            Id = id;
            Role = role;
            Description = description;
        }

        public int Id { get; set; }
        public string Role { get; set; }
        public string? Description { get; set; }

        public static CharacterRoleResponseDto CharacterRoleModelToResponseDto(CharacterRole characterRole)
        {
            return new()
            {
                Id = characterRole.Id,
                Role = characterRole.Role,
                Description = characterRole.Description
            };
        }
    }
}
