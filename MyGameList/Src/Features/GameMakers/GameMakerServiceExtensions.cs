using MyGameList.Src.Features.GameMakers.Repositories;
using MyGameList.Src.Features.GameMakers.Services;

namespace MyGameList.Src.Features.GameMakers
{
    public static class GameMakerServiceExtensions
    {
        public static IServiceCollection AddGameMakerServices(this IServiceCollection services)
        {
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();

            return services;
        }
    }
}
