using MyGameList.Src.Features.Users.Models;
using MyGameList.Src.Shared.Commons;
using System.Text.Json.Serialization;

namespace MyGameList.Src.Features.Users.Dtos
{
    public class UserResponseDto
    {
        public UserResponseDto()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        public UserResponseDto(int id, string username, string email, string password, string name, string? bio,
            string? gender, DateTime? birthday, string? country, string? link)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            Bio = bio;
            Gender = gender;
            Birthday = birthday;
            Country = country;
            Link = link;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
        [JsonConverter(typeof(CustomDateFormatConverter))]
        public DateTime? Birthday { get; set; }
        public string? Country { get; set; }
        public string? Link { get; set; }

        public static UserResponseDto UserModelToResponseDto(User user)
        {
            return new()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Bio = user.Bio,
                Gender = user.Gender?.Option,
                Birthday = user.Birthday,
                Country = user.Country,
                Link = user.Link
            };
        }
    }
}
