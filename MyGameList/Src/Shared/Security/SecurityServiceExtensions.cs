namespace MyGameList.Src.Shared.Security
{
    public static class SecurityServiceExtensions
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordService, PasswordService>();

            return services;
        }
    }
}
