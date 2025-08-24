using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("age_ratings")]
    [Index(nameof(Rating), IsUnique = true)]
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
        [Key]
        public int Id { get; set; }
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
    }
}
