using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("age_ratings")]
    public class AgeRating
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
        public int Id { get; set; }
        [Column("rating")]
        public string Rating { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("age_minimum")]
        public int AgeMinimum { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
