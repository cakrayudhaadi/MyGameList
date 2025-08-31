using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Games.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.GameMakers.Models
{
    [Table("people")]
    public class Person
    {
        public Person()
        {
            Name = string.Empty;
        }

        public Person(int id, string name, string? bio, int? genderId,
            DateTime? birthday, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Name = name;
            Bio = bio;
            GenderId = genderId;
            Birthday = birthday;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("bio")]
        public string? Bio { get; set; }
        [Column("gender_id")]
        public int? GenderId { get; set; }
        [Column("birthday")]
        public DateTime? Birthday { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public Gender? Gender { get; set; }
        public ICollection<Game> Games { get; set; } = [];
    }
}
