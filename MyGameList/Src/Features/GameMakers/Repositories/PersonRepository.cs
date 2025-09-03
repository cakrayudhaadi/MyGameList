using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person, int>
    {
        Task<Person?> IsDataDuplicateAsync(int? id, Person compare);
    }

    public class PersonRepository(MyGameListDbContext context) : GenericRepository<Person, int>(context), IPersonRepository
    {
        public override async Task<List<Person>> GetAllDatasAsync()
        {
            return await context.Person
                .Include(person => person.Gender)
                .ToListAsync();
        }

        public override async Task<Person?> GetDataByIdAsync(int id)
        {
            return await context.Person
                .Include(person => person.Gender)
                .FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task<Person?> IsDataDuplicateAsync(int? id, Person compare)
        {
            if (id.HasValue)
                return await context.Person.FirstOrDefaultAsync(person => person.Id != id && person.Name == compare.Name);
            else
                return await context.Person.FirstOrDefaultAsync(person => person.Name == compare.Name);
        }
    }
}
