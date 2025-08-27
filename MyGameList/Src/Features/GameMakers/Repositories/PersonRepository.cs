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
        Task<Person?> GetPersonByNameAsync(string name);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
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

        public async Task<Person?> GetPersonByNameAsync(string name)
        {
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
    }
}
