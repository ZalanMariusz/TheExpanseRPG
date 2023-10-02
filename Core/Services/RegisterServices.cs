using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    static class RegisterServices
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<DiceRollService>();
        }
    }
}
