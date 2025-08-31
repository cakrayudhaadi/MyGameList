using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Generic.Models;

namespace MyGameList.Src.Features.Generic.Repositories
{
    public interface IGenericRepository<T, TId>
    {
        Task<T> AddAsync(T obj);
        Task<List<T>> GetAllDatasAsync();
        Task<T?> GetDataByIdAsync(TId id);
        Task UpdateDataAsync(T obj);
        Task DeleteDataAsync(T obj);
        Task<List<T>> GetDatasByIdsAsync(List<TId> ids);
    }
    public abstract class GenericRepository<T, TId>(MyGameListDbContext context) : IGenericRepository<T, TId> where T : GenericModel<TId>
    {
        private readonly DbSet<T> dbSet = context.Set<T>();

        public virtual async Task<T> AddAsync(T obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            await dbSet.AddAsync(obj);
            await context.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<List<T>> GetAllDatasAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetDataByIdAsync(TId id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task UpdateDataAsync(T obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            dbSet.Update(obj);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteDataAsync(T obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            dbSet.Remove(obj);
            await context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetDatasByIdsAsync(List<TId> ids)
        {
            return await dbSet
                .Where(obj => ids.Contains(obj.Id))
                .ToListAsync();
        }
    }
}
