using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Services
{
    public interface IPlatformService
    {
        Task<Response<PlatformResponseDto>> AddPlatformAsync(PlatformDto platformDto);
        Task<Response<List<PlatformResponseDto>>> GetAllPlatformsAsync();
        Task<Response<PlatformResponseDto>> GetPlatformByIdAsync(int id);
        Task<Response> UpdatePlatformAsync(int id, PlatformDto platformDto);
        Task<Response> DeletePlatformAsync(int id);
    }

    public class PlatformService(IPlatformRepository platformRepo) : IPlatformService
    {
        public async Task<Response<PlatformResponseDto>> AddPlatformAsync(PlatformDto platformDto)
        {
            ArgumentNullException.ThrowIfNull(platformDto);

            if (string.IsNullOrEmpty(platformDto.Platform))
                return new Response<PlatformResponseDto>(HttpStatusCode.BadRequest, "Platform are required.", null);

            Platform? duplicate = await platformRepo.GetPlatformByOptionAsync(platformDto.Platform);
            if (duplicate is not null)
                return new Response<PlatformResponseDto>(HttpStatusCode.BadRequest, "Platform must be unique.", null);

            Platform platform = platformDto.PlatformDtoToModel(null, null);
            Platform newPlatform = await platformRepo.AddAsync(platform);
            PlatformResponseDto platformResponseDtos = PlatformResponseDto.PlatformModelToResponseDto(newPlatform);

            return new Response<PlatformResponseDto>(HttpStatusCode.OK, "Platform created successfully.", platformResponseDtos);
        }

        public async Task<Response<List<PlatformResponseDto>>> GetAllPlatformsAsync()
        {
            List<Platform> platforms = await platformRepo.GetAllPlatformsAsync();
            List<PlatformResponseDto> platformResponseDtos = [.. platforms.Select(platform => PlatformResponseDto.PlatformModelToResponseDto(platform))];

            return new Response<List<PlatformResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), platformResponseDtos);
        }

        public async Task<Response<PlatformResponseDto>> GetPlatformByIdAsync(int id)
        {
            Platform? platform = await platformRepo.GetPlatformByIdAsync(id);
            if (platform is null)
                return new Response<PlatformResponseDto>(HttpStatusCode.NotFound, "Platform not found.", null);

            PlatformResponseDto platformResponseDtos = PlatformResponseDto.PlatformModelToResponseDto(platform);

            return new Response<PlatformResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), platformResponseDtos);
        }

        public async Task<Response> UpdatePlatformAsync(int id, PlatformDto platformDto)
        {
            Platform? existingPlatform = await platformRepo.GetPlatformByIdAsync(id);
            if (existingPlatform is null)
                return new Response(HttpStatusCode.NotFound, "Platform not found.");

            Platform? duplicate = await platformRepo.GetPlatformByOptionAsync(platformDto.Platform);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Platform must be unique.");

            Platform updatedPlatform = platformDto.PlatformDtoToModel(existingPlatform, id);
            await platformRepo.UpdatePlatformAsync(updatedPlatform);

            return new Response(HttpStatusCode.OK, "Platform updated successfully.");
        }

        public async Task<Response> DeletePlatformAsync(int id)
        {
            Platform? existingPlatform = await platformRepo.GetPlatformByIdAsync(id);
            if (existingPlatform is null)
                return new Response(HttpStatusCode.NotFound, "Platform not found.");

            await platformRepo.DeletePlatformAsync(existingPlatform);

            return new Response(HttpStatusCode.OK, "Platform deleted successfully.");
        }
    }
}