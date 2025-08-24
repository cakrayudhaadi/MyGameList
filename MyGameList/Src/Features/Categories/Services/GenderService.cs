using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Services
{
    public interface IGenderService
    {
        Task<Response<GenderResponseDto>> AddGenderAsync(GenderDto genderDto);
        Task<Response<List<GenderResponseDto>>> GetAllGendersAsync();
        Task<Response<GenderResponseDto>> GetGenderByIdAsync(int id);
        Task<Response> UpdateGenderAsync(int id, GenderDto genderDto);
        Task<Response> DeleteGenderAsync(int id);
    }

    public class GenderService(IGenderRepository genderRepo) : IGenderService
    {
        public async Task<Response<GenderResponseDto>> AddGenderAsync(GenderDto genderDto)
        {
            ArgumentNullException.ThrowIfNull(genderDto);

            if (string.IsNullOrEmpty(genderDto.Gender))
                return new Response<GenderResponseDto>(HttpStatusCode.BadRequest, "Gender are required.", null);

            Gender? duplicate = await genderRepo.GetGenderByOptionAsync(genderDto.Gender);
            if (duplicate is not null)
                return new Response<GenderResponseDto>(HttpStatusCode.BadRequest, "Gender must be unique.", null);

            Gender gender = genderDto.GenderDtoToModel(null, null);
            Gender newGender = await genderRepo.AddAsync(gender);
            GenderResponseDto genderResponseDtos = GenderResponseDto.GenderModelToResponseDto(newGender);

            return new Response<GenderResponseDto>(HttpStatusCode.OK, "Gender created successfully.", genderResponseDtos);
        }

        public async Task<Response<List<GenderResponseDto>>> GetAllGendersAsync()
        {
            List<Gender> genders = await genderRepo.GetAllGendersAsync();
            List<GenderResponseDto> genderResponseDtos = [.. genders.Select(gender => GenderResponseDto.GenderModelToResponseDto(gender))];

            return new Response<List<GenderResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), genderResponseDtos);
        }

        public async Task<Response<GenderResponseDto>> GetGenderByIdAsync(int id)
        {
            Gender? gender = await genderRepo.GetGenderByIdAsync(id);
            if (gender is null)
                return new Response<GenderResponseDto>(HttpStatusCode.NotFound, "Gender not found.", null);

            GenderResponseDto genderResponseDtos = GenderResponseDto.GenderModelToResponseDto(gender);

            return new Response<GenderResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), genderResponseDtos);
        }

        public async Task<Response> UpdateGenderAsync(int id, GenderDto genderDto)
        {
            Gender? existingGender = await genderRepo.GetGenderByIdAsync(id);
            if (existingGender is null)
                return new Response(HttpStatusCode.NotFound, "Gender not found.");

            Gender? duplicate = await genderRepo.GetGenderByOptionAsync(genderDto.Gender);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Gender must be unique.");

            Gender updatedGender = genderDto.GenderDtoToModel(existingGender, id);
            await genderRepo.UpdateGenderAsync(updatedGender);

            return new Response(HttpStatusCode.OK, "Gender updated successfully.");
        }

        public async Task<Response> DeleteGenderAsync(int id)
        {
            Gender? existingGender = await genderRepo.GetGenderByIdAsync(id);
            if (existingGender is null)
                return new Response(HttpStatusCode.NotFound, "Gender not found.");

            await genderRepo.DeleteGenderAsync(existingGender);

            return new Response(HttpStatusCode.OK, "Gender deleted successfully.");
        }
    }
}