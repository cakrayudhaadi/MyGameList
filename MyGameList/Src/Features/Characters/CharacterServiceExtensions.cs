using MyGameList.Src.Features.Characters.Repositories;
using MyGameList.Src.Features.Characters.Services;

namespace MyGameList.Src.Features.Characters
{
    public static class CharacterServiceExtensions
    {
        public static IServiceCollection AddCharacterServices(this IServiceCollection services)
        {
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<ICharacterRoleRepository, CharacterRoleRepository>();
            services.AddScoped<ICharacterRoleService, CharacterRoleService>();
            services.AddScoped<IGameCharacterService, GameCharacterService>();

            return services;
        }
    }
}
