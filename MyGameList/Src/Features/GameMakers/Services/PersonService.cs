using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.GameMakers.Repositories;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.GameMakers.Services
{
    public interface IPersonService
    {
        Task<Response<PersonResponseDto>> AddPersonAsync(PersonDto personDto);
        Task<Response<List<PersonResponseDto>>> GetAllPersonsAsync();
        Task<Response<PersonResponseDto>> GetPersonByIdAsync(int id);
        Task<Response> UpdatePersonAsync(int id, PersonDto personDto);
        Task<Response> DeletePersonAsync(int id);
        Task<List<Person>> GetPersonListByIds(List<int> ids);
    }

    public class PersonService(IPersonRepository personRepo, IGenderService genderService) : IPersonService
    {
        public async Task<Response<PersonResponseDto>> AddPersonAsync(PersonDto personDto)
        {
            ArgumentNullException.ThrowIfNull(personDto);

            string? errValidation = personDto.PersonValidation();
            if (errValidation is not null)
                return new Response<PersonResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Gender? gender = null;
            if (personDto.GenderId.HasValue)
            {
                gender = await genderService.GetGenderById(personDto.GenderId.Value);
                if (gender is null)
                    return new Response<PersonResponseDto>(HttpStatusCode.BadRequest, "Gender not found.", null);
            }

            Person person = personDto.PersonDtoToModel(null, null);
            person.Gender = gender;

            Person newPerson = await personRepo.AddAsync(person);
            PersonResponseDto personResponseDto = PersonResponseDto.PersonModelToResponseDto(newPerson);

            return new Response<PersonResponseDto>(HttpStatusCode.OK, "Person created successfully.", personResponseDto);
        }

        public async Task<Response<List<PersonResponseDto>>> GetAllPersonsAsync()
        {
            List<Person> people = await personRepo.GetAllPersonsAsync();
            List<PersonResponseDto> personResponseDtos = [.. people.Select(person => PersonResponseDto.PersonModelToResponseDto(person))];

            return new Response<List<PersonResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), personResponseDtos);
        }

        public async Task<Response<PersonResponseDto>> GetPersonByIdAsync(int id)
        {
            Person? person = await personRepo.GetPersonByIdAsync(id);
            if (person is null)
                return new Response<PersonResponseDto>(HttpStatusCode.NotFound, "Person not found.", null);

            PersonResponseDto personResponseDto = PersonResponseDto.PersonModelToResponseDto(person);

            return new Response<PersonResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), personResponseDto);
        }

        public async Task<Response> UpdatePersonAsync(int id, PersonDto personDto)
        {
            Person? existingPerson = await personRepo.GetPersonByIdAsync(id);
            if (existingPerson is null)
                return new Response(HttpStatusCode.NotFound, "Person not found.");

            if (personDto.GenderId.HasValue)
            {
                Gender? gender = await genderService.GetGenderById(personDto.GenderId.Value);
                if (gender is null)
                    return new Response(HttpStatusCode.BadRequest, "Gender not found.");
            }

            Person updatedPerson = personDto.PersonDtoToModel(existingPerson, id);
            await personRepo.UpdatePersonAsync(updatedPerson);

            return new Response(HttpStatusCode.OK, "Person updated successfully.");
        }

        public async Task<Response> DeletePersonAsync(int id)
        {
            Person? existingPerson = await personRepo.GetPersonByIdAsync(id);
            if (existingPerson is null)
                return new Response(HttpStatusCode.NotFound, "Person not found.");

            await personRepo.DeletePersonAsync(existingPerson);

            return new Response(HttpStatusCode.OK, "Person deleted successfully.");
        }

        public async Task<List<Person>> GetPersonListByIds(List<int> ids)
        {
            List<Person> people = await personRepo.GetPersonListByIdsAsync(ids);

            return people;
        }
    }
}
