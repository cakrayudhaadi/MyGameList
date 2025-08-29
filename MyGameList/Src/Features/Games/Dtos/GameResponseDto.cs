using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Games.Dtos
{
    public class GameResponseDto
    {
        public GameResponseDto()
        {
            Title = string.Empty;
        }

        public GameResponseDto(int id, string title, string? description, string? plot, int? yearRelease, int? ageRatingId)
        {
            Id = id;
            Title = title;
            Description = description;
            Plot = plot;
            YearRelease = yearRelease;
            AgeRatingId = ageRatingId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Plot { get; set; }
        public int? YearRelease { get; set; }
        public int? AgeRatingId { get; set; }

        public static GameResponseDto GameModelToResponseDto(Game game)
        {
            return new()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Plot = game.Plot,
                YearRelease = game.YearRelease,
                AgeRatingId = game.AgeRatingId
            };
        }
    }
}
