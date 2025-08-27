using MyGameList.Src.Features.GameMakers.Models;

namespace MyGameList.Src.Features.GameMakers.Dtos
{
    public class PublisherResponseDto
    {
        public PublisherResponseDto()
        {
            Name = string.Empty;
        }

        public PublisherResponseDto(int id, string name, string? description, int? establishedIn, string? website)
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

        public static PublisherResponseDto PublisherModelToResponseDto(Publisher publisher)
        {
            return new()
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Description = publisher.Description,
                EstablishedIn = publisher.EstablishedIn,
                Website = publisher.Website
            };
        }

        public static PublisherResponseDto PublisherModelAsOptions(Publisher publisher)
        {
            return new()
            {
                Id = publisher.Id,
                Name = publisher.Name,
            };
        }
    }
}
