using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Users.Dtos;
using MyGameList.Src.Features.Users.Models;
using MyGameList.Src.Features.Users.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;
using System.Reflection;

namespace MyGameList.Src.Features.Users.Services
{
    public interface IUserService
    {
        Task<Response<UserResponseDto>> AddUserAsync(UserDto userDto);
        Task<Response<List<UserResponseDto>>> GetAllUsersAsync();
        Task<Response<UserResponseDto>> GetUserByIdAsync(int id);
        Task<Response> UpdateUserAsync(int id, UserDto userDto);
        Task<Response> DeleteUserAsync(int id);
        Task<List<User>> GetUserListByIds(List<int> ids);
    }
    public class UserService(IUserRepository userRepo, IGenderService genderService) : IUserService
    {
        public async Task<Response<UserResponseDto>> AddUserAsync(UserDto userDto)
        {
            ArgumentNullException.ThrowIfNull(userDto);

            string? errValidation = userDto.UserValidation();
            if (errValidation is not null)
                return new Response<UserResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Gender? gender = null;
            if (userDto.GenderId.HasValue)
            {
                gender = await genderService.GetGenderById(userDto.GenderId.Value);
                if (gender is null)
                    return new Response<UserResponseDto>(HttpStatusCode.BadRequest, "Gender not found.", null);
            }

            User user = userDto.UserDtoToModel(null, null);
            User? duplicate = await userRepo.IsDataDuplicateAsync(null, user);
            if (duplicate is not null)
                return new Response<UserResponseDto>(HttpStatusCode.BadRequest, "Username and Email must be unique.", null);
            user.Gender = gender;

            User newUser = await userRepo.AddAsync(user);
            UserResponseDto userResponseDto = UserResponseDto.UserModelToResponseDto(newUser);

            return new Response<UserResponseDto>(HttpStatusCode.OK, "User created successfully.", userResponseDto);
        }

        public async Task<Response<List<UserResponseDto>>> GetAllUsersAsync()
        {
            List<User> users = await userRepo.GetAllDatasAsync();
            List<UserResponseDto> userResponseDtos = [.. users.Select(user => UserResponseDto.UserModelToResponseDto(user))];

            return new Response<List<UserResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), userResponseDtos);
        }

        public async Task<Response<UserResponseDto>> GetUserByIdAsync(int id)
        {
            User? user = await userRepo.GetDataByIdAsync(id);
            if (user is null)
                return new Response<UserResponseDto>(HttpStatusCode.NotFound, "User not found.", null);

            UserResponseDto userResponseDto = UserResponseDto.UserModelToResponseDto(user);

            return new Response<UserResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), userResponseDto);
        }

        public async Task<Response> UpdateUserAsync(int id, UserDto userDto)
        {
            User? existingUser = await userRepo.GetDataByIdAsync(id);
            if (existingUser is null)
                return new Response(HttpStatusCode.NotFound, "User not found.");

            if (userDto.GenderId.HasValue)
            {
                Gender? gender = await genderService.GetGenderById(userDto.GenderId.Value);
                if (gender is null)
                    return new Response(HttpStatusCode.BadRequest, "Gender not found.");
            }

            User updatedUser = userDto.UserDtoToModel(existingUser, id);
            User? duplicate = await userRepo.IsDataDuplicateAsync(null, updatedUser);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Username and Email must be unique.");

            await userRepo.UpdateDataAsync(updatedUser);

            return new Response(HttpStatusCode.OK, "User updated successfully.");
        }

        public async Task<Response> DeleteUserAsync(int id)
        {
            User? existingUser = await userRepo.GetDataByIdAsync(id);
            if (existingUser is null)
                return new Response(HttpStatusCode.NotFound, "User not found.");

            await userRepo.DeleteDataAsync(existingUser);

            return new Response(HttpStatusCode.OK, "User deleted successfully.");
        }

        public async Task<List<User>> GetUserListByIds(List<int> ids)
        {
            List<User> users = await userRepo.GetDatasByIdsAsync(ids);

            return users;
        }
    }
}
