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
        Task<List<Developer>> GetDeveloperListByIds(List<int> ids);
    }

    public class DeveloperService(IDeveloperRepository developerRepo) : IDeveloperService
    {
        public async Task<Response<DeveloperResponseDto>> AddDeveloperAsync(DeveloperDto developerDto)
        {
            ArgumentNullException.ThrowIfNull(developerDto);

            string? errValidation = developerDto.DeveloperValidation();
            if (errValidation is not null)
                return new Response<DeveloperResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Developer developer = developerDto.DeveloperDtoToModel(null, null);
            Developer? duplicate = await developerRepo.IsDataDuplicateAsync(null, developer);
            if (duplicate is not null)
                return new Response<DeveloperResponseDto>(HttpStatusCode.BadRequest, "Developer Name must be unique.", null);

            Developer newDeveloper = await developerRepo.AddAsync(developer);
            DeveloperResponseDto developerResponseDto = DeveloperResponseDto.DeveloperModelToResponseDto(newDeveloper);

            return new Response<DeveloperResponseDto>(HttpStatusCode.OK, "Developer created successfully.", developerResponseDto);
        }

        public async Task<Response<List<DeveloperResponseDto>>> GetAllDevelopersAsync()
        {
            List<Developer> developers = await developerRepo.GetAllDatasAsync();
            List<DeveloperResponseDto> developerResponseDtos = [.. developers.Select(developer => DeveloperResponseDto.DeveloperModelToResponseDto(developer))];

            return new Response<List<DeveloperResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), developerResponseDtos);
        }

        public async Task<Response<DeveloperResponseDto>> GetDeveloperByIdAsync(int id)
        {
            Developer? developer = await developerRepo.GetDataByIdAsync(id);
            if (developer is null)
                return new Response<DeveloperResponseDto>(HttpStatusCode.NotFound, "Developer not found.", null);

            DeveloperResponseDto developerResponseDto = DeveloperResponseDto.DeveloperModelToResponseDto(developer);

            return new Response<DeveloperResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), developerResponseDto);
        }

        public async Task<Response> UpdateDeveloperAsync(int id, DeveloperDto developerDto)
        {
            Developer? existingDeveloper = await developerRepo.GetDataByIdAsync(id);
            if (existingDeveloper is null)
                return new Response(HttpStatusCode.NotFound, "Developer not found.");

            Developer updatedDeveloper = developerDto.DeveloperDtoToModel(existingDeveloper, id);
            Developer? duplicate = await developerRepo.IsDataDuplicateAsync(id, updatedDeveloper);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Developer Name must be unique.");

            await developerRepo.UpdateDataAsync(updatedDeveloper);

            return new Response(HttpStatusCode.OK, "Developer updated successfully.");
        }

        public async Task<Response> DeleteDeveloperAsync(int id)
        {
            Developer? existingDeveloper = await developerRepo.GetDataByIdAsync(id);
            if (existingDeveloper is null)
                return new Response(HttpStatusCode.NotFound, "Developer not found.");

            await developerRepo.DeleteDataAsync(existingDeveloper);

            return new Response(HttpStatusCode.OK, "Developer deleted successfully.");
        }

        public async Task<List<Developer>> GetDeveloperListByIds(List<int> ids)
        {
            List<Developer> developers = await developerRepo.GetDatasByIdsAsync(ids);

            return developers;
        }
    }
}
