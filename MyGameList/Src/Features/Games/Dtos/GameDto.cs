using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Games.Dtos
{
    public class GameDto
    {
        public GameDto()
        {
        }

        public GameDto(string? title, string? description, string? plot, int? yearRelease)
        {
            Title = title;
            Description = description;
            Plot = plot;
            YearRelease = yearRelease;
        }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Plot { get; set; }
        public int? YearRelease { get; set; }
        public List<int> AgeRatingIds { get; set; } = [];
        public List<int> DeveloperIds { get; set; } = [];
        public List<int> PublisherIds { get; set; } = [];
        public List<int> ProducerIds { get; set; } = [];
        public List<int> GenreIds { get; set; } = [];
        public List<int> ModeIds { get; set; } = [];
        public List<int> PlatformIds { get; set; } = [];
        public List<int> CharacterIds { get; set; } = [];

        public Game GameDtoToModel(Game? game, int? id)
        {
            game ??= new Game();
            DateTime timeNow = DateTime.UtcNow;

            game.Title = !string.IsNullOrEmpty(Title) ? Title : game.Title;
            game.Description = !string.IsNullOrEmpty(Description) ? Description : game.Description;
            game.Plot = !string.IsNullOrEmpty(Plot) ? Plot : game.Plot;
            game.YearRelease = YearRelease.HasValue ? YearRelease : game.YearRelease;
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
