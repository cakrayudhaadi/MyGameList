using MyGameList.Src.Features.Games.Models;
using System.Xml.Linq;

namespace MyGameList.Src.Features.Games.Dtos
{
    public class GameDto
    {
        public GameDto()
        {
            Title = string.Empty;
        }

        public GameDto(string title, string? description, string? plot, int? yearRelease, int? ageRatingId)
        {
            Title = title;
            Description = description;
            Plot = plot;
            YearRelease = yearRelease;
            AgeRatingId = ageRatingId;
        }

        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Plot { get; set; }
        public int? YearRelease { get; set; }
        public int? AgeRatingId { get; set; }

        public Game GamesDtoToModel(Game? game, int? id)
        {
            game ??= new Game();
            DateTime timeNow = DateTime.UtcNow;

            game.Title = Title is not null ? Title : game.Title;
            game.Description = Description is not null ? Description : game.Description;
            game.Plot = Plot is not null ? Plot : game.Plot;
            game.YearRelease = YearRelease is not null ? YearRelease : game.YearRelease;
            game.AgeRatingId = AgeRatingId is not null ? AgeRatingId : game.AgeRatingId;
            if (!id.HasValue) {
                game.CreatedAt = timeNow;
                game.UpdatedAt = timeNow;
            } else {
                game.Id = id.Value;
                game.UpdatedAt = timeNow;
            }

            return game;
        }

        public string? GameValidation()
        {
            if (string.IsNullOrEmpty(Title))
                return "Title is required";

            return null;
        }
    }
}
