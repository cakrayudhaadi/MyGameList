using MyGameList.Src.Features.Categories.Repositories;
using MyGameList.Src.Features.Categories.Services;

namespace MyGameList.Src.Features.Categories
{
    public static class CategoryServiceExtensions
    {
        public static IServiceCollection AddCategoryServices(this IServiceCollection services)
        {
            services.AddScoped<IAgeRatingRepository, AgeRatingRepository>();
            services.AddScoped<IAgeRatingService, AgeRatingService>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IModeRepository, ModeRepository>();
            services.AddScoped<IModeService, ModeService>();
            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<IPlatformService, PlatformService>();

            return services;
        }
    }
}
