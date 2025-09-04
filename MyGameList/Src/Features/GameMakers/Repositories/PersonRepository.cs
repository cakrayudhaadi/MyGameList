using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person, int>
    {
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
    }
}
