using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Characters.Models
{
    [Table("character")]
    public class Character : GenericModel<int>
    {
        public Character()
        {
            Name = string.Empty;
        }

        public Character(int id, string name, string? nickname, string? description, int? characterRoleId,
            int? yearRelease, DateTime? birthday, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Name = name;
            Nickname = nickname;
            Description = description;
            CharacterRoleId = characterRoleId;
            YearRelease = yearRelease;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("nickname")]
        public string? Nickname { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("character_role_id")]
        public int? CharacterRoleId { get; set; }
        [Column("year_release")]
        public int? YearRelease { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public CharacterRole? CharacterRole { get; set; }
        public ICollection<Game> Games { get; set; } = [];

        protected override bool CustomEquals(object other)
        {
            Character otherObj = (Character)other;
            return Name == otherObj.Name
                && Nickname == otherObj.Nickname
                && Description == otherObj.Description
                && CharacterRoleId == otherObj.CharacterRoleId
                && YearRelease == otherObj.YearRelease;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, CharacterRoleId, YearRelease);
        }
    }
}
