using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Games.Dtos
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
            game ??= new Game();
            DateTime timeNow = DateTime.UtcNow;

            game.Title = Title is not null ? Title : game.Title;
            game.Description = Description is not null ? Description : game.Description;
            if(!id.HasValue) {
                game.CreatedAt = timeNow;
                game.UpdatedAt = timeNow;
            } else {
                game.Id = id.Value;
                game.UpdatedAt = timeNow;
            }

            return game;
        }
    }
}
