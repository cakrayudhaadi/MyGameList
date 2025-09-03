using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IPlatformRepository : IGenericRepository<Platform, int>
    {
        Task<Platform?> IsDataDuplicateAsync(int? id, Platform compare);
    }

    public class PlatformRepository(MyGameListDbContext context) : GenericRepository<Platform, int>(context), IPlatformRepository
    {
        public async Task<Platform?> IsDataDuplicateAsync(int? id, Platform compare)
        {
            if (id.HasValue)
                return await context.Platform.FirstOrDefaultAsync(platform => platform.Id != id && platform.Option == compare.Option);
            else
                return await context.Platform.FirstOrDefaultAsync(platform => platform.Option == compare.Option);
        }
    }
}
