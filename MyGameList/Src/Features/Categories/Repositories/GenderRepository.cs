using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Generic.Repositories;

namespace MyGameList.Src.Features.Categories.Repositories
{
    public interface IGenderRepository : IGenericRepository<Gender, int>
    {
        Task<Gender?> IsDataDuplicateAsync(int? id, Gender compare);
    }

    public class GenderRepository(MyGameListDbContext context) : GenericRepository<Gender, int>(context), IGenderRepository
    {
        public async Task<Gender?> IsDataDuplicateAsync(int? id, Gender compare)
        {
            if (id.HasValue)
                return await context.Gender.FirstOrDefaultAsync(gender => gender.Id != id && gender.Option == compare.Option);
            else
                return await context.Gender.FirstOrDefaultAsync(gender => gender.Option == compare.Option);
        }
    }
}
