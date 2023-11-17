using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.Services
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            return services;
        }
    }
}
