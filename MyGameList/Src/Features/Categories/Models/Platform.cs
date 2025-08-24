using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("platforms")]
    [Index(nameof(Option), IsUnique = true)]
    public class Platform
    {
        public Platform()
        {
            Option = string.Empty;
        }

        public Platform(int id, string option, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Option = option;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("option")]
        [Required]
        public string Option { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
