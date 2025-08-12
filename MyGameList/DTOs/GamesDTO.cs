using MyGameList.Models;

namespace My_Game_List.DTOs
{
    public class GamesDTO
    {
        public GamesDTO() {}

        public GamesDTO(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public Games GamesDTOToModel(Games? game, int? id)
        {
            if (game is null) {
                game = new Games();
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
