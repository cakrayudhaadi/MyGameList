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

            developer.Name = !string.IsNullOrEmpty(Name) ? Name : developer.Name;
            developer.Description = !string.IsNullOrEmpty(Description) ? Description : developer.Description;
            developer.EstablishedIn = EstablishedIn.HasValue ? EstablishedIn : developer.EstablishedIn;
            developer.Website = !string.IsNullOrEmpty(Website) ? Website : developer.Website;
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
                return "Name is required";

            return null;
        }
    }
}
