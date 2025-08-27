using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.GameMakers.Models
{
    [Table("developers")]
    [Index(nameof(Name), IsUnique = true)]
    public class Developer
    {
        public Developer()
        {
            Name = string.Empty;
        }

        public Developer(int id, string name, string? description, int? establishedIn,
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
        public int Id { get; set; }
        [Column("rating")]
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
    }
}
