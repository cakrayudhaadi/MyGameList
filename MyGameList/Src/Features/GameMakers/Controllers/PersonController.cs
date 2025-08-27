using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.GameMakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PersonResponseDto>>> AddPerson(PersonDto personDto)
        {
            try
            {
                return res.Result<PersonResponseDto>(await personService.AddPersonAsync(personDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<PersonResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<PersonResponseDto>>>> GetAllPersons()
        {
            try
            {
                return res.Result<List<PersonResponseDto>>(await personService.GetAllPersonsAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<PersonResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PersonResponseDto>>> GetPersonById(int id)
        {
            try
            {
                return res.Result<PersonResponseDto>(await personService.GetPersonByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<PersonResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdatePerson(int id, PersonDto personDto)
        {
            try
            {
                return res.Result(await personService.UpdatePersonAsync(id, personDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePerson(int id)
        {
            try
            {
                return res.Result(await personService.DeletePersonAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
