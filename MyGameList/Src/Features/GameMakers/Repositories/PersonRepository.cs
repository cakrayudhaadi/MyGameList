using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;
using System;

namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> AddAsync(Person person);
        Task<List<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(int id);
        Task<Person?> GetPersonByNameAsync(int? id, string name);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
        Task<List<Person>> GetPersonListByIdsAsync(List<int> ids);
    }

    public class PersonRepository(MyGameListDbContext context) : IPersonRepository
    {
        public async Task<Person> AddAsync(Person person)
        {
            ArgumentNullException.ThrowIfNull(person);
            context.Person.Add(person);
            await context.SaveChangesAsync();
            return person;
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await context.Person.Include(person => person.Gender).ToListAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await context.Person.Include(person => person.Gender).FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task<Person?> GetPersonByNameAsync(int? id, string name)
        {
            if (id.HasValue)
                return await context.Person.FirstOrDefaultAsync(person => person.Id != id && person.Name == name);
            else
                return await context.Person.FirstOrDefaultAsync(person => person.Name == name);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            ArgumentNullException.ThrowIfNull(person);
            context.Person.Update(person);
            await context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person person)
        {
            ArgumentNullException.ThrowIfNull(person);
            context.Person.Remove(person);
            await context.SaveChangesAsync();
        }

        public async Task<List<Person>> GetPersonListByIdsAsync(List<int> ids)
        {
            return await context.Person
                .Where(person => ids.Contains(person.Id))
                .ToListAsync();
        }
    }
}
