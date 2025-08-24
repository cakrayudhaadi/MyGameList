using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController(IGenderService genderService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<GenderResponseDto>>> AddGender(GenderDto genderDto)
        {
            try
            {
                return res.Result<GenderResponseDto>(await genderService.AddGenderAsync(genderDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<GenderResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GenderResponseDto>>>> GetAllGenders()
        {
            try
            {
                return res.Result<List<GenderResponseDto>>(await genderService.GetAllGendersAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<GenderResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GenderResponseDto>>> GetGenderById(int id)
        {
            try
            {
                return res.Result<GenderResponseDto>(await genderService.GetGenderByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<GenderResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateGender(int id, GenderDto genderDto)
        {
            try
            {
                return res.Result(await genderService.UpdateGenderAsync(id, genderDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteGender(int id)
        {
            try
            {
                return res.Result(await genderService.DeleteGenderAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
