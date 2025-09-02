using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyGameList.Src.Features.Characters.Models
{
    [Table("character_roles")]
    public class CharacterRole : GenericModel<int>
    {
        public CharacterRole()
        {
            Role = string.Empty;
        }

        public CharacterRole(int id, string role, string? description, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Role = role;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("role")]
        public string Role { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public ICollection<Character>? Characters { get; set; }

        protected override bool CustomEquals(object other)
        {
            CharacterRole otherObj = (CharacterRole)other;
            return Role == otherObj.Role
                && Description == otherObj.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Role, Description);
        }
    }
}
