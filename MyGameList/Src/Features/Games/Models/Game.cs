using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Games.Models
{
    [Table("Games")]
    public class Game
    {
        public Game()
        {
            Title = string.Empty;
            Description = string.Empty;
        }

        public Game(int id, string title, string description, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
