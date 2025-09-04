using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Users.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Users.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User?> IsDataDuplicateAsync(int? id, User compare);
    }
    public class UserRepository(MyGameListDbContext context) : GenericRepository<User, int>(context), IUserRepository
    {
        public override async Task<List<User>> GetAllDatasAsync()
        {
            return await context.User
                .Include(user => user.Gender)
                .ToListAsync();
        }

        public override async Task<User?> GetDataByIdAsync(int id)
        {
            return await context.User
                .Include(user => user.Gender)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> IsDataDuplicateAsync(int? id, User compare)
        {
            if (id.HasValue)
                return await context.User.FirstOrDefaultAsync(user => user.Id != id
                    && (user.Username == compare.Username
                    || user.Email == compare.Email));
            else
                return await context.User.FirstOrDefaultAsync(user => (user.Username == compare.Username
                    && user.Email == compare.Email));
        }
    }
}
