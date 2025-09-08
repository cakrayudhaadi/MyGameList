using MyGameList.Src.Features.Users.Models;
using MyGameList.Src.Shared.Commons;
using System.Text.Json.Serialization;

namespace MyGameList.Src.Features.Users.Dtos
{
    public class UserDto
    {
        public UserDto()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        public UserDto(string username, string email, string password, string name, string? bio, int? genderId,
            DateTime? birthday, string? country, string? link)
        {
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            Bio = bio;
            GenderId = genderId;
            Birthday = birthday;
            Country = country;
            Link = link;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public int? GenderId { get; set; }
        [JsonConverter(typeof(CustomDateFormatConverter))]
        public DateTime? Birthday { get; set; }
        public string? Country { get; set; }
        public string? Link { get; set; }

        public User UserDtoToModel(User? user, int? id)
        {
            user ??= new User();
            DateTime timeNow = DateTime.UtcNow;

            user.Username = !string.IsNullOrEmpty(Username) ? Username : user.Username;
            user.Email = !string.IsNullOrEmpty(Email) ? Email : user.Email;
            user.Password = !string.IsNullOrEmpty(Password) ? Password : user.Password;
            user.Name = !string.IsNullOrEmpty(Name) ? Name : user.Name;
            user.Bio = !string.IsNullOrEmpty(Bio) ? Bio : user.Bio;
            user.GenderId = GenderId.HasValue ? GenderId : user.GenderId;
            user.Birthday = Birthday.HasValue ? Birthday : user.Birthday;
            user.Country = !string.IsNullOrEmpty(Country) ? Country : user.Country;
            user.Link = !string.IsNullOrEmpty(Link) ? Link : user.Link;
            if (!id.HasValue)
            {
                user.CreatedAt = timeNow;
                user.UpdatedAt = timeNow;
            }
            else
            {
                user.Id = id.Value;
                user.UpdatedAt = timeNow;
            }

            return user;
        }

        public string? UserValidation()
        {
            if (string.IsNullOrEmpty(Username))
                return "Username is required";
            if (string.IsNullOrEmpty(Email))
                return "Email is required";
            if (string.IsNullOrEmpty(Password))
                return "Password is required";
            if (string.IsNullOrEmpty(Name))
                return "Name is required";

            return null;
        }
    }
}
