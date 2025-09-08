using MyGameList.Src.Features.Users.Models;
using MyGameList.Src.Shared.Commons;
using System.Text.Json.Serialization;

namespace MyGameList.Src.Features.Users.Dtos
{
    public class SignUpDto
    {
        public SignUpDto()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        public SignUpDto(string username, string email, string password, string name)
        {
            Username = username;
            Email = email;
            Password = password;
            Name = name;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User DtoToModel()
        {
            User user = new();
            DateTime timeNow = DateTime.UtcNow;

            user.Username = !string.IsNullOrEmpty(Username) ? Username : user.Username;
            user.Email = !string.IsNullOrEmpty(Email) ? Email : user.Email;
            user.Name = !string.IsNullOrEmpty(Name) ? Name : user.Name;
            user.CreatedAt = timeNow;
            user.UpdatedAt = timeNow;

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
