using MyGameList.Src.Features.GameMakers.Models;

namespace MyGameList.Src.Features.GameMakers.Dtos
{
    public class DeveloperResponseDto
    {
        public DeveloperResponseDto()
        {
            Name = string.Empty;
        }

        public DeveloperResponseDto(int id, string name, string? description, int? establishedIn, string? website)
        {
            Id = id;
            Name = name;
            Description = description;
            EstablishedIn = establishedIn;
            Website = website;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? EstablishedIn { get; set; }
        public string? Website { get; set; }

        public static DeveloperResponseDto DeveloperModelToResponseDto(Developer developer)
        {
            return new()
            {
                Id = developer.Id,
                Name = developer.Name,
                Description = developer.Description,
                EstablishedIn = developer.EstablishedIn,
                Website = developer.Website
            };
        }

        public static DeveloperResponseDto DeveloperModelAsOptions(Developer developer)
        {
            return new()
            {
                Id = developer.Id,
                Name = developer.Name,
            };
        }
    }
}
