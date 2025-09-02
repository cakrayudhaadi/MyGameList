using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("age_ratings")]
    [Index(nameof(Rating), IsUnique = true)]
    public class AgeRating : GenericModel<int>
    {
        public AgeRating()
        {
            Rating = string.Empty;
            AgeMinimum = 0;
        }

        public AgeRating(int id, string rating, string? description, int ageMinimum, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Rating = rating;
            Description = description;
            AgeMinimum = ageMinimum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        [Key]
        public new int Id { get; set; }
        [Column("rating")]
        [Required]
        public string Rating { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("age_minimum")]
        [Required]
        public int AgeMinimum { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Game> Games { get; set; } = [];

        protected override bool CustomEquals(object other)
        {
            AgeRating otherObj = (AgeRating)other;
            return Rating == otherObj.Rating
                && Description == otherObj.Description
                && AgeMinimum == otherObj.AgeMinimum;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Rating, Description, AgeMinimum);
        }
    }
}
