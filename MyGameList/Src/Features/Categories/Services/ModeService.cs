using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Services
{
    public interface IModeService
    {
        Task<Response<ModeResponseDto>> AddModeAsync(ModeDto modeDto);
        Task<Response<List<ModeResponseDto>>> GetAllModesAsync();
        Task<Response<ModeResponseDto>> GetModeByIdAsync(int id);
        Task<Response> UpdateModeAsync(int id, ModeDto modeDto);
        Task<Response> DeleteModeAsync(int id);
        Task<List<Mode>> GetModeListByIds(List<int> ids);
    }

    public class ModeService(IModeRepository modeRepo) : IModeService
    {
        public async Task<Response<ModeResponseDto>> AddModeAsync(ModeDto modeDto)
        {
            ArgumentNullException.ThrowIfNull(modeDto);

            string? errValidation = modeDto.ModeValidation();
            if (errValidation is not null)
                return new Response<ModeResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Mode? duplicate = await modeRepo.GetModeByOptionAsync(null, modeDto.Mode);
            if (duplicate is not null)
                return new Response<ModeResponseDto>(HttpStatusCode.BadRequest, "Mode must be unique.", null);

            Mode mode = modeDto.ModeDtoToModel(null, null);
            Mode newMode = await modeRepo.AddAsync(mode);
            ModeResponseDto modeResponseDto = ModeResponseDto.ModeModelToResponseDto(newMode);

            return new Response<ModeResponseDto>(HttpStatusCode.OK, "Mode created successfully.", modeResponseDto);
        }

        public async Task<Response<List<ModeResponseDto>>> GetAllModesAsync()
        {
            List<Mode> modes = await modeRepo.GetAllModesAsync();
            List<ModeResponseDto> modeResponseDtos = [.. modes.Select(mode => ModeResponseDto.ModeModelToResponseDto(mode))];

            return new Response<List<ModeResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), modeResponseDtos);
        }

        public async Task<Response<ModeResponseDto>> GetModeByIdAsync(int id)
        {
            Mode? mode = await modeRepo.GetModeByIdAsync(id);
            if (mode is null)
                return new Response<ModeResponseDto>(HttpStatusCode.NotFound, "Mode not found.", null);

            ModeResponseDto modeResponseDto = ModeResponseDto.ModeModelToResponseDto(mode);

            return new Response<ModeResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), modeResponseDto);
        }

        public async Task<Response> UpdateModeAsync(int id, ModeDto modeDto)
        {
            Mode? existingMode = await modeRepo.GetModeByIdAsync(id);
            if (existingMode is null)
                return new Response(HttpStatusCode.NotFound, "Mode not found.");

            Mode? duplicate = await modeRepo.GetModeByOptionAsync(id, modeDto.Mode);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Mode must be unique.");

            Mode updatedMode = modeDto.ModeDtoToModel(existingMode, id);
            await modeRepo.UpdateModeAsync(updatedMode);

            return new Response(HttpStatusCode.OK, "Mode updated successfully.");
        }

        public async Task<Response> DeleteModeAsync(int id)
        {
            Mode? existingMode = await modeRepo.GetModeByIdAsync(id);
            if (existingMode is null)
                return new Response(HttpStatusCode.NotFound, "Mode not found.");

            await modeRepo.DeleteModeAsync(existingMode);

            return new Response(HttpStatusCode.OK, "Mode deleted successfully.");
        }

        public async Task<List<Mode>> GetModeListByIds(List<int> ids)
        {
            List<Mode> modes = await modeRepo.GetModeListByIdsAsync(ids);

            return modes;
        }
    }
}