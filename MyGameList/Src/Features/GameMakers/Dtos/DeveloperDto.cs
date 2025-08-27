using MyGameList.Src.Features.GameMakers.Models;

namespace MyGameList.Src.Features.GameMakers.Dtos
{
    public class DeveloperDto
    {
        public DeveloperDto()
        {
            Name = string.Empty;
        }

        public DeveloperDto(string name, string? description, int? establishedIn, string? website)
        {
            Name = name;
            Description = description;
            EstablishedIn = establishedIn;
            Website = website;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
        public int? EstablishedIn { get; set; }
        public string? Website { get; set; }

        public Developer DeveloperDtoToModel(Developer? developer, int? id)
        {
            developer ??= new Developer();
            DateTime timeNow = DateTime.UtcNow;

            developer.Name = Name is not null ? Name : developer.Name;
            developer.Description = Description is not null ? Description : developer.Description;
            developer.EstablishedIn = EstablishedIn is not null ? EstablishedIn : developer.EstablishedIn;
            developer.Website = Website is not null ? Website : developer.Website;
            if (!id.HasValue)
            {
                developer.CreatedAt = timeNow;
                developer.UpdatedAt = timeNow;
            }
            else
            {
                developer.Id = id.Value;
                developer.UpdatedAt = timeNow;
            }

            return developer;
        }

        public string? DeveloperValidation()
        {
            if (string.IsNullOrEmpty(Name))
                return "Name are required";

            return null;
        }
    }
}
