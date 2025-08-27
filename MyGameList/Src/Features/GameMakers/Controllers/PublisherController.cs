using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.GameMakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController(IPublisherService publisherService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PublisherResponseDto>>> AddPublisher(PublisherDto publisherDto)
        {
            try
            {
                return res.Result<PublisherResponseDto>(await publisherService.AddPublisherAsync(publisherDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<PublisherResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<PublisherResponseDto>>>> GetAllPublishers()
        {
            try
            {
                return res.Result<List<PublisherResponseDto>>(await publisherService.GetAllPublishersAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<PublisherResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PublisherResponseDto>>> GetPublisherById(int id)
        {
            try
            {
                return res.Result<PublisherResponseDto>(await publisherService.GetPublisherByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<PublisherResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdatePublisher(int id, PublisherDto publisherDto)
        {
            try
            {
                return res.Result(await publisherService.UpdatePublisherAsync(id, publisherDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePublisher(int id)
        {
            try
            {
                return res.Result(await publisherService.DeletePublisherAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
