using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("modes")]
    [Index(nameof(Option), IsUnique = true)]
    public class Mode : GenericModel<int>
    {
        public Mode()
        {
            Option = string.Empty;
        }

        public Mode(int id, string option, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Option = option;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        [Key]
        public new int Id { get; set; }
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
            Mode otherObj = (Mode)other;
            return Option == otherObj.Option;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Option);
        }
    }
}
