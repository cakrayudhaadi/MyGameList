using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Models;

namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPublisherRepository
    {
        Task<Publisher> AddAsync(Publisher publisher);
        Task<List<Publisher>> GetAllPublishersAsync();
        Task<Publisher?> GetPublisherByIdAsync(int id);
        Task<Publisher?> GetPublisherByNameAsync(string name);
        Task UpdatePublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(Publisher publisher);
    }

    public class PublisherRepository(MyGameListDbContext context) : IPublisherRepository
    {
        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            ArgumentNullException.ThrowIfNull(publisher);
            context.Publisher.Add(publisher);
            await context.SaveChangesAsync();
            return publisher;
        }

        public async Task<List<Publisher>> GetAllPublishersAsync()
        {
            return await context.Publisher.ToListAsync();
        }

        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            return await context.Publisher.FindAsync(id);
        }

        public async Task<Publisher?> GetPublisherByNameAsync(string name)
        {
            return await context.Publisher.FirstOrDefaultAsync(publisher => publisher.Name == name);
        }

        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            ArgumentNullException.ThrowIfNull(publisher);
            context.Publisher.Update(publisher);
            await context.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(Publisher publisher)
        {
            ArgumentNullException.ThrowIfNull(publisher);
            context.Publisher.Remove(publisher);
            await context.SaveChangesAsync();
        }
    }
}
