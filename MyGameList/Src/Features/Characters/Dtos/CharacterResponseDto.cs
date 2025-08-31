using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.Games.Dtos;

namespace MyGameList.Src.Features.Characters.Dtos
{
    public class CharacterResponseDto
    {
        public CharacterResponseDto()
        {
        }

        public CharacterResponseDto(int id, string? name, string? nickname, string? description, int? characterRoleId, int? yearRelease)
        {
            Id = id;
            Name = name;
            Nickname = nickname;
            Description = description;
            CharacterRoleId = characterRoleId;
            YearRelease = yearRelease;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Description { get; set; }
        public int? CharacterRoleId { get; set; }
        public int? YearRelease { get; set; }
        public ICollection<GameCoverResponseDto> Games { get; set; } = [];

        public static CharacterResponseDto CharacterModelToResponseDto(Character? character)
        {
            ArgumentNullException.ThrowIfNull(character);

            CharacterResponseDto characterResponseDto = new()
            {
                Id = character.Id,
                Name = character.Name,
                Nickname = character.Nickname,
                Description = character.Description,
                CharacterRoleId = character.CharacterRoleId,
                YearRelease = character.YearRelease,
                Games = [.. character.Games.Select(game => GameCoverResponseDto.GameModelToCoverResponseDto(game))]
            };

            return characterResponseDto;
        }
    }
    public class CharacterCoverResponseDto
    {
        public CharacterCoverResponseDto()
        {
        }

        public CharacterCoverResponseDto(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public static CharacterCoverResponseDto CharacterModelToCoverResponseDto(Character character)
        {
            CharacterCoverResponseDto characterCoverResponseDto = new()
            {
                Id = character.Id,
                Name = character.Name
            };

            return characterCoverResponseDto;
        }
    }
}
