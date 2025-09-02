using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPublisherRepository : IGenericRepository<Publisher, int>
    {
        Task<Publisher?> IsDataDuplicateAsync(int? id, Publisher compare);
    }

    public class PublisherRepository(MyGameListDbContext context) : GenericRepository<Publisher, int>(context), IPublisherRepository
    {
        public async Task<Publisher?> IsDataDuplicateAsync(int? id, Publisher compare)
        {
            if (id.HasValue)
                return await context.Publisher.FirstOrDefaultAsync(publisher => publisher.Id != id && publisher.Name == compare.Name);
            else
                return await context.Publisher.FirstOrDefaultAsync(publisher => publisher.Name == compare.Name);
        }
    }
}
