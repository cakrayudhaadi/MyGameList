using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Characters.Dtos;
using MyGameList.Src.Features.Characters.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Characters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(ICharacterService characterService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddCharacter(CharacterDto characterDto)
        {
            try
            {
                return res.Result(await characterService.AddCharacterAsync(characterDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CharacterCoverResponseDto>>>> GetAllCharacters()
        {
            try
            {
                return res.Result<List<CharacterCoverResponseDto>>(await characterService.GetAllCharactersAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<CharacterCoverResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CharacterResponseDto>>> GetCharacterById(int id)
        {
            try
            {
                return res.Result<CharacterResponseDto>(await characterService.GetCharacterByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<CharacterResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateCharacter(int id, CharacterDto characterDto)
        {
            try
            {
                return res.Result(await characterService.UpdateCharacterAsync(id, characterDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteCharacter(int id)
        {
            try
            {
                return res.Result(await characterService.DeleteCharacterAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
