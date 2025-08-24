using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeController(IModeService modeService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ModeResponseDto>>> AddMode(ModeDto modeDto)
        {
            try
            {
                return res.Result<ModeResponseDto>(await modeService.AddModeAsync(modeDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<ModeResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ModeResponseDto>>>> GetAllModes()
        {
            try
            {
                return res.Result<List<ModeResponseDto>>(await modeService.GetAllModesAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<ModeResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ModeResponseDto>>> GetModeById(int id)
        {
            try
            {
                return res.Result<ModeResponseDto>(await modeService.GetModeByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<ModeResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateMode(int id, ModeDto modeDto)
        {
            try
            {
                return res.Result(await modeService.UpdateModeAsync(id, modeDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteMode(int id)
        {
            try
            {
                return res.Result(await modeService.DeleteModeAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
