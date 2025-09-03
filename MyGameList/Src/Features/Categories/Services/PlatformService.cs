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
        Task<List<Platform>> GetPlatformListByIds(List<int> ids);
    }

    public class PlatformService(IPlatformRepository platformRepo) : IPlatformService
    {
        public async Task<Response<PlatformResponseDto>> AddPlatformAsync(PlatformDto platformDto)
        {
            ArgumentNullException.ThrowIfNull(platformDto);

            string? errValidation = platformDto.PlatformValidation();
            if (errValidation is not null)
                return new Response<PlatformResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Platform platform = platformDto.PlatformDtoToModel(null, null);
            Platform? duplicate = await platformRepo.IsDataDuplicateAsync(null, platform);
            if (duplicate is not null)
                return new Response<PlatformResponseDto>(HttpStatusCode.BadRequest, "Platform must be unique.", null);

            Platform newPlatform = await platformRepo.AddAsync(platform);
            PlatformResponseDto platformResponseDto = PlatformResponseDto.PlatformModelToResponseDto(newPlatform);

            return new Response<PlatformResponseDto>(HttpStatusCode.OK, "Platform created successfully.", platformResponseDto);
        }

        public async Task<Response<List<PlatformResponseDto>>> GetAllPlatformsAsync()
        {
            List<Platform> platforms = await platformRepo.GetAllDatasAsync();
            List<PlatformResponseDto> platformResponseDtos = [.. platforms.Select(platform => PlatformResponseDto.PlatformModelToResponseDto(platform))];

            return new Response<List<PlatformResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), platformResponseDtos);
        }

        public async Task<Response<PlatformResponseDto>> GetPlatformByIdAsync(int id)
        {
            Platform? platform = await platformRepo.GetDataByIdAsync(id);
            if (platform is null)
                return new Response<PlatformResponseDto>(HttpStatusCode.NotFound, "Platform not found.", null);

            PlatformResponseDto platformResponseDto = PlatformResponseDto.PlatformModelToResponseDto(platform);

            return new Response<PlatformResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), platformResponseDto);
        }

        public async Task<Response> UpdatePlatformAsync(int id, PlatformDto platformDto)
        {
            Platform? existingPlatform = await platformRepo.GetDataByIdAsync(id);
            if (existingPlatform is null)
                return new Response(HttpStatusCode.NotFound, "Platform not found.");

            Platform updatedPlatform = platformDto.PlatformDtoToModel(existingPlatform, id);
            Platform? duplicate = await platformRepo.IsDataDuplicateAsync(id, updatedPlatform);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Platform must be unique.");

            await platformRepo.UpdateDataAsync(updatedPlatform);

            return new Response(HttpStatusCode.OK, "Platform updated successfully.");
        }

        public async Task<Response> DeletePlatformAsync(int id)
        {
            Platform? existingPlatform = await platformRepo.GetDataByIdAsync(id);
            if (existingPlatform is null)
                return new Response(HttpStatusCode.NotFound, "Platform not found.");

            await platformRepo.DeleteDataAsync(existingPlatform);

            return new Response(HttpStatusCode.OK, "Platform deleted successfully.");
        }

        public async Task<List<Platform>> GetPlatformListByIds(List<int> ids)
        {
            List<Platform> platforms = await platformRepo.GetDatasByIdsAsync(ids);

            return platforms;
        }
    }
}