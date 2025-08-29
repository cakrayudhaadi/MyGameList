using MyGameList.Src.Shared.Commons;
using MyGameList.Src.Features.GameMakers.Models;
using System.Text.Json.Serialization;

namespace MyGameList.Src.Features.GameMakers.Dtos
{
    public class PersonResponseDto
    {
        public PersonResponseDto()
        {
            Name = string.Empty;
        }

        public PersonResponseDto(int id, string name, string? bio, string? gender, DateTime? birthday)
        {
            Name = name;
            Bio = bio;
            Gender = gender;
            Birthday = birthday;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
        [JsonConverter(typeof(CustomDateFormatConverter))]
        public DateTime? Birthday { get; set; }

        public static PersonResponseDto PersonModelToResponseDto(Person person)
        {
            return new()
            {
                Id = person.Id,
                Name = person.Name,
                Bio = person.Bio,
                Gender = person.Gender?.Option,
                Birthday = person.Birthday
            };
        }

        public static PersonResponseDto PersonModelAsOptions(Person person)
        {
            return new()
            {
                Id = person.Id,
                Name = person.Name,
            };
        }

        public static PersonResponseDto PersonModelToGameProperties(Person person)
        {
            return new()
            {
                Name = person.Name
            };
        }

        public static PersonResponseDto PersonModelToEditGameProperties(Person person)
        {
            return new()
            {
                Id = person.Id,
                Name = person.Name
            };
        }
    }
}
