using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.MVVM.ViewModel;

namespace TheExpanseRPG.Core.MVVM.View
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterViews(this IServiceCollection services)
        {
            services.AddSingleton<WelcomeSplashWindow>();
            services.AddSingleton<PlayerMainWindow>();
            services.AddSingleton<GmMainWindow>();
            services.AddTransient<TalentInfoWindow>();

            services.AddTransient<CharacterCreationWindow>();
            services.AddTransient<TalentListWindow>();

            return services;
        }
    }
}
