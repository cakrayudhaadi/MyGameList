using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IModeRepository : IGenericRepository<Mode, int>
    {
        Task<Mode?> IsDataDuplicateAsync(int? id, Mode compare);
    }

    public class ModeRepository(MyGameListDbContext context) : GenericRepository<Mode, int>(context), IModeRepository
    {
        public async Task<Mode?> IsDataDuplicateAsync(int? id, Mode compare)
        {
            if (id.HasValue)
                return await context.Mode.FirstOrDefaultAsync(mode => mode.Id != id && mode.Option == compare.Option);
            else
                return await context.Mode.FirstOrDefaultAsync(mode => mode.Option == compare.Option);
        }
    }
}