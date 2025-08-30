using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Characters.Dtos;
using MyGameList.Src.Features.Characters.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Characters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterRoleController(ICharacterRoleService characterRoleService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CharacterRoleResponseDto>>> AddCharacterRole(CharacterRoleDto characterRoleDto)
        {
            try
            {
                return res.Result<CharacterRoleResponseDto>(await characterRoleService.AddCharacterRoleAsync(characterRoleDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<CharacterRoleResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CharacterRoleResponseDto>>>> GetAllCharacterRoles()
        {
            try
            {
                return res.Result<List<CharacterRoleResponseDto>>(await characterRoleService.GetAllCharacterRolesAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<CharacterRoleResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CharacterRoleResponseDto>>> GetCharacterRoleById(int id)
        {
            try
            {
                return res.Result<CharacterRoleResponseDto>(await characterRoleService.GetCharacterRoleByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<CharacterRoleResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateCharacterRole(int id, CharacterRoleDto characterRoleDto)
        {
            try
            {
                return res.Result(await characterRoleService.UpdateCharacterRoleAsync(id, characterRoleDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteCharacterRole(int id)
        {
            try
            {
                return res.Result(await characterRoleService.DeleteCharacterRoleAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
