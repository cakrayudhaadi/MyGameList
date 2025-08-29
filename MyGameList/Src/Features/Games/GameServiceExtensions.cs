using MyGameList.Src.Features.Games.Repositories;
using MyGameList.Src.Features.Games.Services;

namespace MyGameList.Src.Features.Games
{
    public static class GameServiceExtensions
    {
        public static IServiceCollection AddGameServices(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
