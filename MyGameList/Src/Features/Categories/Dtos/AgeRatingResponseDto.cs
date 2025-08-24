using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class AgeRatingResponseDto
    {
        public AgeRatingResponseDto()
        {
            Rating = string.Empty;
            AgeMinimum = 0;
        }

        public AgeRatingResponseDto(int id, string rating, string? description, int ageMinimum)
        {
            Id = id;
            Rating = rating;
            Description = description;
            AgeMinimum = ageMinimum;
        }

        public int Id { get; set; }
        public string Rating { get; set; }
        public string? Description { get; set; }
        public int AgeMinimum { get; set; }

        public static AgeRatingResponseDto AgeRatingModelToResponseDto(AgeRating ageRating)
        {
            return new()
            {
                Id = ageRating.Id,
                Rating = ageRating.Rating,
                Description = ageRating.Description,
                AgeMinimum = ageRating.AgeMinimum
            };
        }

        public static AgeRatingResponseDto AgeRatingModelAsOptions(AgeRating ageRating)
        {
            return new()
            {
                Id = ageRating.Id,
                Rating = ageRating.Rating
            };
        }
    }
}
