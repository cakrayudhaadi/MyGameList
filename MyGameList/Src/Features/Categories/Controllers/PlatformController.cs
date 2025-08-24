using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController(IPlatformService platformService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PlatformResponseDto>>> AddPlatform(PlatformDto platformDto)
        {
            try
            {
                return res.Result<PlatformResponseDto>(await platformService.AddPlatformAsync(platformDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<PlatformResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<PlatformResponseDto>>>> GetAllPlatforms()
        {
            try
            {
                return res.Result<List<PlatformResponseDto>>(await platformService.GetAllPlatformsAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<PlatformResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PlatformResponseDto>>> GetPlatformById(int id)
        {
            try
            {
                return res.Result<PlatformResponseDto>(await platformService.GetPlatformByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<PlatformResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdatePlatform(int id, PlatformDto platformDto)
        {
            try
            {
                return res.Result(await platformService.UpdatePlatformAsync(id, platformDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePlatform(int id)
        {
            try
            {
                return res.Result(await platformService.DeletePlatformAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
