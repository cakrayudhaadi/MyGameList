using MyGameList.Src.Features.GameMakers.Models;

namespace MyGameList.Src.Features.GameMakers.Dtos
{
    public class PublisherDto
    {
        public PublisherDto()
        {
            Name = string.Empty;
        }

        public PublisherDto(string name, string? description, int? establishedIn, string? website)
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

        public Publisher PublisherDtoToModel(Publisher? publisher, int? id)
        {
            publisher ??= new Publisher();
            DateTime timeNow = DateTime.UtcNow;

            publisher.Name = Name is not null ? Name : publisher.Name;
            publisher.Description = Description is not null ? Description : publisher.Description;
            publisher.EstablishedIn = EstablishedIn is not null ? EstablishedIn : publisher.EstablishedIn;
            publisher.Website = Website is not null ? Website : publisher.Website;
            if (!id.HasValue)
            {
                publisher.CreatedAt = timeNow;
                publisher.UpdatedAt = timeNow;
            }
            else
            {
                publisher.Id = id.Value;
                publisher.UpdatedAt = timeNow;
            }

            return publisher;
        }

        public string? PublisherValidation()
        {
            if (string.IsNullOrEmpty(Name))
                return "Name are required";

            return null;
        }
    }
}
