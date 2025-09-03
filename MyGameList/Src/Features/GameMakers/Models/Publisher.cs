using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.GameMakers.Models
{
    [Table("publishers")]
    [Index(nameof(Name), IsUnique = true)]
    public class Publisher : GenericModel<int>
    {
        public Publisher()
        {
            Name = string.Empty;
        }

        public Publisher(int id, string name, string? description, int? establishedIn,
            string? website, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Name = name;
            Description = description;
            EstablishedIn = establishedIn;
            Website = website;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        [Key]
        public new int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("established_in")]
        [Required]
        public int? EstablishedIn { get; set; }
        [Column("website")]
        public string? Website { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Game> Games { get; set; } = [];

        protected override bool CustomEquals(object other)
        {
            Publisher otherObj = (Publisher)other;
            return Name == otherObj.Name
                && Description == otherObj.Description
                && EstablishedIn == otherObj.EstablishedIn
                && Website == otherObj.Website;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, EstablishedIn, Website);
        }
    }
}
