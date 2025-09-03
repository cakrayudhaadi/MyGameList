using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IDeveloperRepository : IGenericRepository<Developer, int>
    {
        Task<Developer?> IsDataDuplicateAsync(int? id, Developer compare);
    }

    public class DeveloperRepository(MyGameListDbContext context) : GenericRepository<Developer, int>(context), IDeveloperRepository
    {
        public async Task<Developer?> IsDataDuplicateAsync(int? id, Developer compare)
        {
            if (id.HasValue)
                return await context.Developer.FirstOrDefaultAsync(developer => developer.Id != id && developer.Name == compare.Name);
            else
                return await context.Developer.FirstOrDefaultAsync(developer => developer.Name == compare.Name);
        }
    }
}
