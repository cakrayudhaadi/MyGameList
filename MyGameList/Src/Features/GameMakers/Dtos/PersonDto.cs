using MyGameList.Src.Shared.Commons;
using MyGameList.Src.Features.GameMakers.Models;
using System.Text.Json.Serialization;

namespace MyGameList.Src.Features.GameMakers.Dtos
{
    public class PersonDto
    {
        public PersonDto()
        {
            Name = string.Empty;
        }

        public PersonDto(string name, string? bio, int? genderId, DateTime? birthday)
        {
            Name = name;
            Bio = bio;
            GenderId = genderId;
            Birthday = birthday;
        }

        public string Name { get; set; }
        public string? Bio { get; set; }
        public int? GenderId { get; set; }
        [JsonConverter(typeof(CustomDateFormatConverter))]
        public DateTime? Birthday { get; set; }

        public Person PersonDtoToModel(Person? person, int? id)
        {
            person ??= new Person();
            DateTime timeNow = DateTime.UtcNow;

            person.Name = Name is not null ? Name : person.Name;
            person.Bio = Bio is not null ? Bio : person.Bio;
            person.GenderId = GenderId is not null ? GenderId : person.GenderId;
            person.Birthday = Birthday is not null ? Birthday : person.Birthday;
            if (!id.HasValue)
            {
                person.CreatedAt = timeNow;
                person.UpdatedAt = timeNow;
            }
            else
            {
                person.Id = id.Value;
                person.UpdatedAt = timeNow;
            }

            return person;
        }

        public string? PersonValidation()
        {
            if (string.IsNullOrEmpty(Name))
                return "Name is required";
            if (!GenderId.HasValue)
                return "GenderId is required";

            return null;
        }
    }
}
