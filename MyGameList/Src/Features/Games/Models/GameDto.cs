namespace MyGameList.Src.Features.Games.Models
{
    public class GameDto
    {
        public GameDto()
        {
            Title = string.Empty;
            Description = string.Empty;
        }

        public GameDto(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public Game GamesDtoToModel(Game? game, int? id)
        {
            if (game is null) {
                game = new Game();
            }

            game.Title = Title is not null ? Title : game.Title;
            game.Description = Description is not null ? Description : game.Description;
            if(!id.HasValue) {
                game.CreatedAt = DateTime.UtcNow;
                game.UpdatedAt = DateTime.UtcNow;
            } else {
                game.Id = id.Value;
                game.UpdatedAt = DateTime.UtcNow;
            }

            return game;
        }
    }
}
