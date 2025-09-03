using MyGameList.Src.Features.Characters.Dtos;
using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.Characters.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Characters.Services
{
    public interface ICharacterRoleService
    {
        Task<Response<CharacterRoleResponseDto>> AddCharacterRoleAsync(CharacterRoleDto characterRoleDto);
        Task<Response<List<CharacterRoleResponseDto>>> GetAllCharacterRolesAsync();
        Task<Response<CharacterRoleResponseDto>> GetCharacterRoleByIdAsync(int id);
        Task<Response> UpdateCharacterRoleAsync(int id, CharacterRoleDto characterRoleDto);
        Task<Response> DeleteCharacterRoleAsync(int id);
        Task<CharacterRole?> GetCharacterRoleById(int id);
    }

    public class CharacterRoleService(ICharacterRoleRepository characterRoleRepo) : ICharacterRoleService
    {
        public async Task<Response<CharacterRoleResponseDto>> AddCharacterRoleAsync(CharacterRoleDto characterRoleDto)
        {
            ArgumentNullException.ThrowIfNull(characterRoleDto);

            string? errValidation = characterRoleDto.CharacterRoleValidation();
            if (errValidation is not null)
                return new Response<CharacterRoleResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            CharacterRole characterRole = characterRoleDto.CharacterRoleDtoToModel(null, null);
            CharacterRole? duplicate = await characterRoleRepo.IsDataDuplicateAsync(null, characterRole);
            if (duplicate is not null)
                return new Response<CharacterRoleResponseDto>(HttpStatusCode.BadRequest, "Role must be unique.", null);

            CharacterRole newCharacterRole = await characterRoleRepo.AddAsync(characterRole);
            CharacterRoleResponseDto characterRoleResponseDto = CharacterRoleResponseDto.CharacterRoleModelToResponseDto(newCharacterRole);

            return new Response<CharacterRoleResponseDto>(HttpStatusCode.OK, "Character Role created successfully.", characterRoleResponseDto);
        }

        public async Task<Response<List<CharacterRoleResponseDto>>> GetAllCharacterRolesAsync()
        {
            List<CharacterRole> characterRoles = await characterRoleRepo.GetAllDatasAsync();
            List<CharacterRoleResponseDto> characterRoleResponseDtos = [.. characterRoles.Select(characterRole => CharacterRoleResponseDto.CharacterRoleModelToResponseDto(characterRole))];

            return new Response<List<CharacterRoleResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), characterRoleResponseDtos);
        }

        public async Task<Response<CharacterRoleResponseDto>> GetCharacterRoleByIdAsync(int id)
        {
            CharacterRole? characterRole = await characterRoleRepo.GetDataByIdAsync(id);
            if (characterRole is null)
                return new Response<CharacterRoleResponseDto>(HttpStatusCode.NotFound, "Character Role not found.", null);

            CharacterRoleResponseDto characterRoleResponseDto = CharacterRoleResponseDto.CharacterRoleModelToResponseDto(characterRole);

            return new Response<CharacterRoleResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), characterRoleResponseDto);
        }

        public async Task<Response> UpdateCharacterRoleAsync(int id, CharacterRoleDto characterRoleDto)
        {
            CharacterRole? existingCharacterRole = await characterRoleRepo.GetDataByIdAsync(id);
            if (existingCharacterRole is null)
                return new Response(HttpStatusCode.NotFound, "Character Role not found.");

            CharacterRole updatedCharacterRole = characterRoleDto.CharacterRoleDtoToModel(existingCharacterRole, id);
            CharacterRole? duplicate = await characterRoleRepo.IsDataDuplicateAsync(id, updatedCharacterRole);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Role must be unique.");

            await characterRoleRepo.UpdateDataAsync(updatedCharacterRole);

            return new Response(HttpStatusCode.OK, "Character Role updated successfully.");
        }

        public async Task<Response> DeleteCharacterRoleAsync(int id)
        {
            CharacterRole? existingCharacterRole = await characterRoleRepo.GetDataByIdAsync(id);
            if (existingCharacterRole is null)
                return new Response(HttpStatusCode.NotFound, "Character Role not found.");

            await characterRoleRepo.DeleteDataAsync(existingCharacterRole);

            return new Response(HttpStatusCode.OK, "Character Role deleted successfully.");
        }

        public async Task<CharacterRole?> GetCharacterRoleById(int id)
        {
            CharacterRole? characterRole = await characterRoleRepo.GetDataByIdAsync(id);

            return characterRole;
        }
    }
}
