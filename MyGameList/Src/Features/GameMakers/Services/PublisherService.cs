using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.GameMakers.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.GameMakers.Services
{
    public interface IPublisherService
    {
        Task<Response<PublisherResponseDto>> AddPublisherAsync(PublisherDto publisherDto);
        Task<Response<List<PublisherResponseDto>>> GetAllPublishersAsync();
        Task<Response<PublisherResponseDto>> GetPublisherByIdAsync(int id);
        Task<Response> UpdatePublisherAsync(int id, PublisherDto publisherDto);
        Task<Response> DeletePublisherAsync(int id);
    }

    public class PublisherService(IPublisherRepository publisherRepo) : IPublisherService
    {
        public async Task<Response<PublisherResponseDto>> AddPublisherAsync(PublisherDto publisherDto)
        {
            ArgumentNullException.ThrowIfNull(publisherDto);

            string? errValidation = publisherDto.PublisherValidation();
            if (errValidation is not null)
                return new Response<PublisherResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Publisher? duplicate = await publisherRepo.GetPublisherByNameAsync(publisherDto.Name);
            if (duplicate is not null)
                return new Response<PublisherResponseDto>(HttpStatusCode.BadRequest, "Publisher Name must be unique.", null);

            Publisher publisher = publisherDto.PublisherDtoToModel(null, null);
            Publisher newPublisher = await publisherRepo.AddAsync(publisher);
            PublisherResponseDto publisherResponseDto = PublisherResponseDto.PublisherModelToResponseDto(newPublisher);

            return new Response<PublisherResponseDto>(HttpStatusCode.OK, "Publisher created successfully.", publisherResponseDto);
        }

        public async Task<Response<List<PublisherResponseDto>>> GetAllPublishersAsync()
        {
            List<Publisher> publishers = await publisherRepo.GetAllPublishersAsync();
            List<PublisherResponseDto> publisherResponseDtos = [.. publishers.Select(publisher => PublisherResponseDto.PublisherModelToResponseDto(publisher))];

            return new Response<List<PublisherResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), publisherResponseDtos);
        }

        public async Task<Response<PublisherResponseDto>> GetPublisherByIdAsync(int id)
        {
            Publisher? publisher = await publisherRepo.GetPublisherByIdAsync(id);
            if (publisher is null)
                return new Response<PublisherResponseDto>(HttpStatusCode.NotFound, "Publisher not found.", null);

            PublisherResponseDto publisherResponseDto = PublisherResponseDto.PublisherModelToResponseDto(publisher);

            return new Response<PublisherResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), publisherResponseDto);
        }

        public async Task<Response> UpdatePublisherAsync(int id, PublisherDto publisherDto)
        {
            Publisher? existingPublisher = await publisherRepo.GetPublisherByIdAsync(id);
            if (existingPublisher is null)
                return new Response(HttpStatusCode.NotFound, "Publisher not found.");

            Publisher? duplicate = await publisherRepo.GetPublisherByNameAsync(publisherDto.Name);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Publisher Name must be unique.");

            Publisher updatedPublisher = publisherDto.PublisherDtoToModel(existingPublisher, id);
            await publisherRepo.UpdatePublisherAsync(updatedPublisher);

            return new Response(HttpStatusCode.OK, "Publisher updated successfully.");
        }

        public async Task<Response> DeletePublisherAsync(int id)
        {
            Publisher? existingPublisher = await publisherRepo.GetPublisherByIdAsync(id);
            if (existingPublisher is null)
                return new Response(HttpStatusCode.NotFound, "Publisher not found.");

            await publisherRepo.DeletePublisherAsync(existingPublisher);

            return new Response(HttpStatusCode.OK, "Publisher deleted successfully.");
        }
    }
}
