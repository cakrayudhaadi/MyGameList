using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Characters.Models;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Games.Models
{
    [Table("games")]
    [Index(nameof(Title), IsUnique = true)]
    public class Game : GenericModel<int>
    {
        public Game()
        {
            Title = string.Empty;
        }

        public Game(int id, string title, string? description, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        [Required]
        public string Title { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("plot")]
        public string? Plot { get; set; }
        [Column("year_release")]
        public int? YearRelease { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<AgeRating> AgeRatings { get; set; } = [];
        public ICollection<Developer> Developers { get; set; } = [];
        public ICollection<Publisher> Publishers { get; set; } = [];
        public ICollection<Person> Producers { get; set; } = [];
        public ICollection<Genre> Genres { get; set; } = [];
        public ICollection<Mode> Modes { get; set; } = [];
        public ICollection<Platform> Platforms { get; set; } = [];
        public ICollection<Character> Characters { get; set; } = [];

        protected override bool CustomEquals(object other)
        {
            Game otherObj = (Game)other;
            return Title == otherObj.Title
                && Description == otherObj.Description
                && Plot == otherObj.Plot
                && YearRelease == otherObj.YearRelease;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Description, Plot, YearRelease);
        }
    }
}
