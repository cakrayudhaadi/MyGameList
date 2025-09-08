using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Generic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGameList.Src.Features.Users.Models
{
    [Table("users")]
    [Index(nameof(Username), nameof(Email), IsUnique = true)]
    public class User : GenericModel<int>
    {
        public User()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        public User(int id, string username, string email, string password, string name, string? bio, int? genderId,
            DateTime? birthday, string? country, string? link, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            Bio = bio;
            GenderId = genderId;
            Birthday = birthday;
            Country = country;
            Link = link;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Column("id")]
        [Key]
        public new int Id { get; set; }
        [Column("username")]
        [Required]
        public string Username { get; set; }
        [Column("email")]
        [Required]
        public string Email { get; set; }
        [Column("password")]
        [Required]
        public string Password { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("bio")]
        public string? Bio { get; set; }
        [Column("gender_id")]
        public int? GenderId { get; set; }
        [Column("birthday")]
        public DateTime? Birthday { get; set; }
        [Column("country")]
        public string? Country { get; set; }
        [Column("link")]
        public string? Link { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public Gender? Gender { get; set; }

        protected override bool CustomEquals(object other)
        {
            User otherObj = (User)other;
            return Username == otherObj.Username
                && Email == otherObj.Email
                && Name == otherObj.Name
                && Bio == otherObj.Bio
                && GenderId == otherObj.GenderId
                && Birthday == otherObj.Birthday
                && Country == otherObj.Country
                && Link == otherObj.Link;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username, Email, Name, Bio, GenderId, Country, Link);
        }
    }
}
