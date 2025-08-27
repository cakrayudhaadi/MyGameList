using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.GameMakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController(IDeveloperService developerService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DeveloperResponseDto>>> AddDeveloper(DeveloperDto developerDto)
        {
            try
            {
                return res.Result<DeveloperResponseDto>(await developerService.AddDeveloperAsync(developerDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<DeveloperResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<DeveloperResponseDto>>>> GetAllDevelopers()
        {
            try
            {
                return res.Result<List<DeveloperResponseDto>>(await developerService.GetAllDevelopersAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<DeveloperResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DeveloperResponseDto>>> GetDeveloperById(int id)
        {
            try
            {
                return res.Result<DeveloperResponseDto>(await developerService.GetDeveloperByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<DeveloperResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateDeveloper(int id, DeveloperDto developerDto)
        {
            try
            {
                return res.Result(await developerService.UpdateDeveloperAsync(id, developerDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteDeveloper(int id)
        {
            try
            {
                return res.Result(await developerService.DeleteDeveloperAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
