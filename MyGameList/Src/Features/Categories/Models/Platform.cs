using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("platforms")]
    [Index(nameof(Option), IsUnique = true)]
    public class Platform : GenericModel<int>
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

        public ICollection<Game> Games { get; set; } = [];

        protected override bool CustomEquals(object other)
        {
            Platform otherObj = (Platform)other;
            return Option == otherObj.Option;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Option);
        }
    }
}
