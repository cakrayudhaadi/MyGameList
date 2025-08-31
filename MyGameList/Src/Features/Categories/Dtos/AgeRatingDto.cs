using MyGameList.Src.Features.Categories.Models;
using System.Data;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class AgeRatingDto
    {
        public AgeRatingDto()
        {
            Rating = string.Empty;
            AgeMinimum = 0;
        }

        public AgeRatingDto(string rating, string? description, int ageMinimum)
        {
            Rating = rating;
            Description = description;
            AgeMinimum = ageMinimum;
        }

        public string Rating { get; set; }
        public string? Description { get; set; }
        public int AgeMinimum { get; set; }

        public AgeRating AgeRatingDtoToModel(AgeRating? ageRating, int? id)
        {
            ageRating ??= new AgeRating();
            DateTime timeNow = DateTime.UtcNow;

            ageRating.Rating = !string.IsNullOrEmpty(Rating) ? Rating : ageRating.Rating;
            ageRating.Description = !string.IsNullOrEmpty(Description) ? Description : ageRating.Description;
            ageRating.AgeMinimum = AgeMinimum;
            if (!id.HasValue)
            {
                ageRating.CreatedAt = timeNow;
                ageRating.UpdatedAt = timeNow;
            }
            else
            {
                ageRating.Id = id.Value;
                ageRating.UpdatedAt = timeNow;
            }

            return ageRating;
        }

        public string? AgeRatingValidation()
        {
            if (string.IsNullOrEmpty(Rating))
                return "Rating is required";

            return null;
        }
    }
}
