using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.GameMakers.Models
{
    [Table("people")]
    public class Person : GenericModel<int>
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

        protected override bool CustomEquals(object other)
        {
            Person otherObj = (Person)other;
            return Name == otherObj.Name
                && Bio == otherObj.Bio
                && GenderId == otherObj.GenderId
                && Birthday == otherObj.Birthday;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Bio, GenderId, Birthday);
        }
    }
}
