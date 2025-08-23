using MyGameList.Src.Features.Categories.Repositories;
using MyGameList.Src.Features.Categories.Services;

namespace MyGameList.Src.Features.Categories
{
    public static class CategoryServiceExtensions
    {
        public static IServiceCollection AddCategoryServices(this IServiceCollection services)
        {
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            return services;
        }
    }
}
