using MyGameList.Src.Features.Characters.Dtos;
using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.Characters.Repositories;
using MyGameList.Src.Features.Games.Services;
using MyGameList.Src.Shared.Commons;
using MyGameList.Src.Shared.Utils;
using System.Net;

namespace MyGameList.Src.Features.Characters.Services
{
    public interface ICharacterService
    {
        Task<Response> AddCharacterAsync(CharacterDto characterDto);
        Task<Response<List<CharacterCoverResponseDto>>> GetAllCharactersAsync();
        Task<Response<CharacterResponseDto>> GetCharacterByIdAsync(int id);
        Task<Response> UpdateCharacterAsync(int id, CharacterDto characterDto);
        Task<Response> DeleteCharacterAsync(int id);
    }

    public class CharacterService(ICharacterRepository characterRepo,
        ICharacterRoleService characterRoleService,
        IGameCharacterService gameCharacterService) : ICharacterService
    {
        public async Task AddOrUpdateCharacterFromDto(Character character, CharacterDto characterDto, int? id)
        {
            await Util.AddCollectionProperties(character.Games, characterDto.GameIds, gameCharacterService.GetGameListByIds);

            if (!id.HasValue)
            {
                await characterRepo.AddAsync(character);
            }
            else
            {
                await characterRepo.UpdateDataAsync(character);
            }
        }

        public async Task<Response> AddCharacterAsync(CharacterDto characterDto)
        {
            ArgumentNullException.ThrowIfNull(characterDto);

            string? errValidation = characterDto.CharacterValidation();
            if (errValidation is not null)
                return new Response(HttpStatusCode.BadRequest, errValidation);

            Character character = characterDto.CharacterDtoToModel(null, null);
            if (characterDto.CharacterRoleId.HasValue)
            {
                CharacterRole? characterRole = await characterRoleService.GetCharacterRoleById(characterDto.CharacterRoleId.Value);
                if (characterRole is null)
                    return new Response(HttpStatusCode.BadRequest, "Character Role not found.");
            }

            await AddOrUpdateCharacterFromDto(character, characterDto, null);

            return new Response(HttpStatusCode.OK, "Character created successfully.");
        }

        public async Task<Response<List<CharacterCoverResponseDto>>> GetAllCharactersAsync()
        {
            List<Character> characters = await characterRepo.GetAllDatasAsync();
            List<CharacterCoverResponseDto> characterResponseDtos = [.. characters.Select(character => CharacterCoverResponseDto.CharacterModelToCoverResponseDto(character))];

            return new Response<List<CharacterCoverResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), characterResponseDtos);
        }

        public async Task<Response<CharacterResponseDto>> GetCharacterByIdAsync(int id)
        {
            Character? character = await characterRepo.GetDataByIdAsync(id);
            if (character is null)
                return new Response<CharacterResponseDto>(HttpStatusCode.NotFound, "Character not found.", null);

            CharacterResponseDto characterResponseDto = CharacterResponseDto.CharacterModelToResponseDto(character);

            return new Response<CharacterResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), characterResponseDto);
        }

        public async Task<Response> UpdateCharacterAsync(int id, CharacterDto characterDto)
        {
            Character? existingCharacter = await characterRepo.GetDataByIdAsync(id);
            if (existingCharacter is null)
                return new Response(HttpStatusCode.NotFound, "Character not found.");

            Character character = characterDto.CharacterDtoToModel(existingCharacter, id);
            if (characterDto.CharacterRoleId.HasValue)
            {
                CharacterRole? characterRole = await characterRoleService.GetCharacterRoleById(characterDto.CharacterRoleId.Value);
                if (characterRole is null)
                    return new Response(HttpStatusCode.BadRequest, "Character Role not found.");
            }

            await AddOrUpdateCharacterFromDto(character, characterDto, id);

            return new Response(HttpStatusCode.OK, "Character updated successfully.");
        }

        public async Task<Response> DeleteCharacterAsync(int id)
        {
            Character? existingCharacter = await characterRepo.GetDataByIdAsync(id);
            if (existingCharacter is null)
                return new Response(HttpStatusCode.NotFound, "Character not found.");

            await characterRepo.DeleteDataAsync(existingCharacter);

            return new Response(HttpStatusCode.OK, "Character deleted successfully.");
        }
    }
}
