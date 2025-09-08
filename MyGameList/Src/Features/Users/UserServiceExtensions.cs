using MyGameList.Src.Features.Users.Repositories;
using MyGameList.Src.Features.Users.Services;

namespace MyGameList.Src.Features.Users
{
    public static class UserServiceExtensions
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
