using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyGameList.Src.Features.Categories.Models
{
    [Table("genders")]
    [Index(nameof(Option), IsUnique = true)]
    public class Gender : GenericModel<int>
    {
        public Gender()
        {
            Option = string.Empty;
        }

        public Gender(int id, string option, DateTime? createdAt, DateTime? updatedAt)
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
        
        [JsonIgnore]
        public ICollection<Person>? Peoples { get; set; }

        protected override bool CustomEquals(object other)
        {
            Gender otherObj = (Gender)other;
            return Option == otherObj.Option;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Option);
        }
    }
}
