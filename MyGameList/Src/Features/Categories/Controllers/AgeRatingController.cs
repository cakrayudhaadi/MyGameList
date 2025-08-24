using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeRatingController(IAgeRatingService ageRatingService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AgeRatingResponseDto>>> AddAgeRating(AgeRatingDto ageRatingDto)
        {
            try
            {
                return res.Result<AgeRatingResponseDto>(await ageRatingService.AddAgeRatingAsync(ageRatingDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<AgeRatingResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<AgeRatingResponseDto>>>> GetAllAgeRatings()
        {
            try
            {
                return res.Result<List<AgeRatingResponseDto>>(await ageRatingService.GetAllAgeRatingsAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<AgeRatingResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AgeRatingResponseDto>>> GetAgeRatingById(int id)
        {
            try
            {
                return res.Result<AgeRatingResponseDto>(await ageRatingService.GetAgeRatingByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<AgeRatingResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateAgeRating(int id, AgeRatingDto ageRatingDto)
        {
            try
            {
                return res.Result(await ageRatingService.UpdateAgeRatingAsync(id, ageRatingDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteAgeRating(int id)
        {
            try
            {
                return res.Result(await ageRatingService.DeleteAgeRatingAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
