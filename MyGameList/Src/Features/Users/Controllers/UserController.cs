using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.Users.Dtos;
using MyGameList.Src.Features.Users.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> AddUser(UserDto userDto)
        {
            try
            {
                return res.Result<UserResponseDto>(await userService.AddUserAsync(userDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result<UserResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserResponseDto>>>> GetAllUsers()
        {
            try
            {
                return res.Result<List<UserResponseDto>>(await userService.GetAllUsersAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<UserResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetUserById(int id)
        {
            try
            {
                return res.Result<UserResponseDto>(await userService.GetUserByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<UserResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateUser(int id, UserDto userDto)
        {
            try
            {
                return res.Result(await userService.UpdateUserAsync(id, userDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteUser(int id)
        {
            try
            {
                return res.Result(await userService.DeleteUserAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
