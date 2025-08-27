using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.GameMakers.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.GameMakers.Services
{
    public interface IDeveloperService
    {
        Task<Response<DeveloperResponseDto>> AddDeveloperAsync(DeveloperDto developerDto);
        Task<Response<List<DeveloperResponseDto>>> GetAllDevelopersAsync();
        Task<Response<DeveloperResponseDto>> GetDeveloperByIdAsync(int id);
        Task<Response> UpdateDeveloperAsync(int id, DeveloperDto developerDto);
        Task<Response> DeleteDeveloperAsync(int id);
    }

    public class DeveloperService(IDeveloperRepository developerRepo) : IDeveloperService
    {
        public async Task<Response<DeveloperResponseDto>> AddDeveloperAsync(DeveloperDto developerDto)
        {
            ArgumentNullException.ThrowIfNull(developerDto);

            string? errValidation = developerDto.DeveloperValidation();
            if (errValidation is not null)
                return new Response<DeveloperResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Developer? duplicate = await developerRepo.GetDeveloperByNameAsync(developerDto.Name);
            if (duplicate is not null)
                return new Response<DeveloperResponseDto>(HttpStatusCode.BadRequest, "Developer Name must be unique.", null);

            Developer developer = developerDto.DeveloperDtoToModel(null, null);
            Developer newDeveloper = await developerRepo.AddAsync(developer);
            DeveloperResponseDto developerResponseDtos = DeveloperResponseDto.DeveloperModelToResponseDto(newDeveloper);

            return new Response<DeveloperResponseDto>(HttpStatusCode.OK, "Developer created successfully.", developerResponseDtos);
        }

        public async Task<Response<List<DeveloperResponseDto>>> GetAllDevelopersAsync()
        {
            List<Developer> developers = await developerRepo.GetAllDevelopersAsync();
            List<DeveloperResponseDto> developerResponseDtos = [.. developers.Select(developer => DeveloperResponseDto.DeveloperModelToResponseDto(developer))];

            return new Response<List<DeveloperResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), developerResponseDtos);
        }

        public async Task<Response<DeveloperResponseDto>> GetDeveloperByIdAsync(int id)
        {
            Developer? developer = await developerRepo.GetDeveloperByIdAsync(id);
            if (developer is null)
                return new Response<DeveloperResponseDto>(HttpStatusCode.NotFound, "Developer not found.", null);

            DeveloperResponseDto developerResponseDtos = DeveloperResponseDto.DeveloperModelToResponseDto(developer);

            return new Response<DeveloperResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), developerResponseDtos);
        }

        public async Task<Response> UpdateDeveloperAsync(int id, DeveloperDto developerDto)
        {
            Developer? existingDeveloper = await developerRepo.GetDeveloperByIdAsync(id);
            if (existingDeveloper is null)
                return new Response(HttpStatusCode.NotFound, "Developer not found.");

            Developer? duplicate = await developerRepo.GetDeveloperByNameAsync(developerDto.Name);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Developer Name must be unique.");

            Developer updatedDeveloper = developerDto.DeveloperDtoToModel(existingDeveloper, id);
            await developerRepo.UpdateDeveloperAsync(updatedDeveloper);

            return new Response(HttpStatusCode.OK, "Developer updated successfully.");
        }

        public async Task<Response> DeleteDeveloperAsync(int id)
        {
            Developer? existingDeveloper = await developerRepo.GetDeveloperByIdAsync(id);
            if (existingDeveloper is null)
                return new Response(HttpStatusCode.NotFound, "Developer not found.");

            await developerRepo.DeleteDeveloperAsync(existingDeveloper);

            return new Response(HttpStatusCode.OK, "Developer deleted successfully.");
        }
    }
}
