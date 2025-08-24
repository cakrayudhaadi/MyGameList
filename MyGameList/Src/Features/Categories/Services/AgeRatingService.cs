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
    }

    public class AgeRatingService(IAgeRatingRepository ageRatingRepo) : IAgeRatingService
    {
        public async Task<Response<AgeRatingResponseDto>> AddAgeRatingAsync(AgeRatingDto ageRatingDto)
        {
            ArgumentNullException.ThrowIfNull(ageRatingDto);

            if (string.IsNullOrEmpty(ageRatingDto.Rating))
                return new Response<AgeRatingResponseDto>(HttpStatusCode.BadRequest, "Rating are required.", null);

            AgeRating? duplicate = await ageRatingRepo.GetAgeRatingByRatingAsync(ageRatingDto.Rating);
            if (duplicate is not null)
                return new Response<AgeRatingResponseDto>(HttpStatusCode.BadRequest, "AgeRating must be unique.", null);

            AgeRating ageRating = ageRatingDto.AgeRatingDtoToModel(null, null);
            AgeRating newAgeRating = await ageRatingRepo.AddAsync(ageRating);
            AgeRatingResponseDto ageRatingResponseDtos = AgeRatingResponseDto.AgeRatingModelToResponseDto(newAgeRating);

            return new Response<AgeRatingResponseDto>(HttpStatusCode.OK, "AgeRating created successfully.", ageRatingResponseDtos);
        }

        public async Task<Response<List<AgeRatingResponseDto>>> GetAllAgeRatingsAsync()
        {
            List<AgeRating> ageRatings = await ageRatingRepo.GetAllAgeRatingsAsync();
            List<AgeRatingResponseDto> ageRatingResponseDtos = [.. ageRatings.Select(ageRating => AgeRatingResponseDto.AgeRatingModelToResponseDto(ageRating))];

            return new Response<List<AgeRatingResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), ageRatingResponseDtos);
        }

        public async Task<Response<AgeRatingResponseDto>> GetAgeRatingByIdAsync(int id)
        {
            AgeRating? ageRating = await ageRatingRepo.GetAgeRatingByIdAsync(id);
            if (ageRating is null)
                return new Response<AgeRatingResponseDto>(HttpStatusCode.NotFound, "AgeRating not found.", null);

            AgeRatingResponseDto ageRatingResponseDtos = AgeRatingResponseDto.AgeRatingModelToResponseDto(ageRating);

            return new Response<AgeRatingResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), ageRatingResponseDtos);
        }

        public async Task<Response> UpdateAgeRatingAsync(int id, AgeRatingDto ageRatingDto)
        {
            AgeRating? existingAgeRating = await ageRatingRepo.GetAgeRatingByIdAsync(id);
            if (existingAgeRating is null)
                return new Response(HttpStatusCode.NotFound, "AgeRating not found.");

            AgeRating? duplicate = await ageRatingRepo.GetAgeRatingByRatingAsync(ageRatingDto.Rating);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "AgeRating must be unique.");

            AgeRating updatedAgeRating = ageRatingDto.AgeRatingDtoToModel(existingAgeRating, id);
            await ageRatingRepo.UpdateAgeRatingAsync(updatedAgeRating);

            return new Response(HttpStatusCode.OK, "AgeRating updated successfully.");
        }

        public async Task<Response> DeleteAgeRatingAsync(int id)
        {
            AgeRating? existingAgeRating = await ageRatingRepo.GetAgeRatingByIdAsync(id);
            if (existingAgeRating is null)
                return new Response(HttpStatusCode.NotFound, "AgeRating not found.");

            await ageRatingRepo.DeleteAgeRatingAsync(existingAgeRating);

            return new Response(HttpStatusCode.OK, "AgeRating deleted successfully.");
        }
    }
}
