using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Categories.Services
{
    public interface IAgeRatingService
    {
        Task<Response<AgeRatingResponseDto>> AddAgeRatingAsync(AgeRatingDto ageRatingDto);
        Task<Response<List<AgeRatingResponseDto>>> GetAllAgeRatingsAsync();
        Task<Response<AgeRatingResponseDto>> GetAgeRatingByIdAsync(int id);
        Task<Response> UpdateAgeRatingAsync(int id, AgeRatingDto ageRatingDto);
        Task<Response> DeleteAgeRatingAsync(int id);
        Task<AgeRating?> GetAgeRatingById(int id);
        Task<List<AgeRating>> GetAgeRatingListByIds(List<int> ids);
    }

    public class AgeRatingService(IAgeRatingRepository ageRatingRepo) : IAgeRatingService
    {
        public async Task<Response<AgeRatingResponseDto>> AddAgeRatingAsync(AgeRatingDto ageRatingDto)
        {
            ArgumentNullException.ThrowIfNull(ageRatingDto);

            string? errValidation = ageRatingDto.AgeRatingValidation();
            if (errValidation is not null)
                return new Response<AgeRatingResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            AgeRating ageRating = ageRatingDto.AgeRatingDtoToModel(null, null);
            AgeRating? duplicate = await ageRatingRepo.IsDataDuplicateAsync(null, ageRating);
            if (duplicate is not null)
                return new Response<AgeRatingResponseDto>(HttpStatusCode.BadRequest, "Age Rating must be unique.", null);

            AgeRating newAgeRating = await ageRatingRepo.AddAsync(ageRating);
            AgeRatingResponseDto ageRatingResponseDto = AgeRatingResponseDto.AgeRatingModelToResponseDto(newAgeRating);

            return new Response<AgeRatingResponseDto>(HttpStatusCode.OK, "Age Rating created successfully.", ageRatingResponseDto);
        }

        public async Task<Response<List<AgeRatingResponseDto>>> GetAllAgeRatingsAsync()
        {
            List<AgeRating> ageRatings = await ageRatingRepo.GetAllDatasAsync();
            List<AgeRatingResponseDto> ageRatingResponseDtos = [.. ageRatings.Select(ageRating => AgeRatingResponseDto.AgeRatingModelToResponseDto(ageRating))];

            return new Response<List<AgeRatingResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), ageRatingResponseDtos);
        }

        public async Task<Response<AgeRatingResponseDto>> GetAgeRatingByIdAsync(int id)
        {
            AgeRating? ageRating = await ageRatingRepo.GetDataByIdAsync(id);
            if (ageRating is null)
                return new Response<AgeRatingResponseDto>(HttpStatusCode.NotFound, "Age Rating not found.", null);

            AgeRatingResponseDto ageRatingResponseDto = AgeRatingResponseDto.AgeRatingModelToResponseDto(ageRating);

            return new Response<AgeRatingResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), ageRatingResponseDto);
        }

        public async Task<Response> UpdateAgeRatingAsync(int id, AgeRatingDto ageRatingDto)
        {
            AgeRating? existingAgeRating = await ageRatingRepo.GetDataByIdAsync(id);
            if (existingAgeRating is null)
                return new Response(HttpStatusCode.NotFound, "Age Rating not found.");

            AgeRating updatedAgeRating = ageRatingDto.AgeRatingDtoToModel(existingAgeRating, id);
            AgeRating? duplicate = await ageRatingRepo.IsDataDuplicateAsync(id, updatedAgeRating);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Age Rating must be unique.");

            await ageRatingRepo.UpdateDataAsync(updatedAgeRating);

            return new Response(HttpStatusCode.OK, "Age Rating updated successfully.");
        }

        public async Task<Response> DeleteAgeRatingAsync(int id)
        {
            AgeRating? existingAgeRating = await ageRatingRepo.GetDataByIdAsync(id);
            if (existingAgeRating is null)
                return new Response(HttpStatusCode.NotFound, "Age Rating not found.");

            await ageRatingRepo.DeleteDataAsync(existingAgeRating);

            return new Response(HttpStatusCode.OK, "Age Rating deleted successfully.");
        }

        public async Task<AgeRating?> GetAgeRatingById(int id)
        {
            AgeRating? ageRating = await ageRatingRepo.GetDataByIdAsync(id);

            return ageRating;
        }

        public async Task<List<AgeRating>> GetAgeRatingListByIds(List<int> ids)
        {
            List<AgeRating> ageRatings = await ageRatingRepo.GetDatasByIdsAsync(ids);

            return ageRatings;
        }
    }
}
