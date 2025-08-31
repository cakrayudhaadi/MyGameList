using MyGameList.Src.Features.Characters.Models;

namespace MyGameList.Src.Features.Characters.Dtos
{
    public class CharacterDto
    {
        public CharacterDto()
        {
        }

        public CharacterDto(string? name, string? nickname, string? description, int? characterRoleId, int? yearRelease)
        {
            Name = name;
            Nickname = nickname;
            Description = description;
            CharacterRoleId = characterRoleId;
            YearRelease = yearRelease;
        }

        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Description { get; set; }
        public int? CharacterRoleId { get; set; }
        public int? YearRelease { get; set; }
        public List<int> GameIds { get; set; } = [];

        public Character CharacterDtoToModel(Character? character, int? id)
        {
            character ??= new Character();
            DateTime timeNow = DateTime.UtcNow;

            character.Name = !string.IsNullOrEmpty(Name) ? Name : character.Name;
            character.Nickname = !string.IsNullOrEmpty(Nickname) ? Nickname : character.Nickname;
            character.Description = !string.IsNullOrEmpty(Description) ? Description : character.Description;
            character.CharacterRoleId = CharacterRoleId.HasValue ? CharacterRoleId : character.CharacterRoleId;
            character.YearRelease = YearRelease.HasValue ? YearRelease : character.YearRelease;
            if (!id.HasValue)
            {
                character.CreatedAt = timeNow;
                character.UpdatedAt = timeNow;
            }
            else
            {
                character.Id = id.Value;
                character.UpdatedAt = timeNow;
            }

            return character;
        }

        public string? CharacterValidation()
        {
            if (string.IsNullOrEmpty(Name))
                return "Name is required";

            return null;
        }
    }
}
