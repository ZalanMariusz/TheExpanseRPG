using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.MVVM.View;

namespace TheExpanseRPG.Core.MVVM.View
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterViews(this IServiceCollection services)
        {
            services.AddSingleton<WelcomeSplashWindow>();
            services.AddTransient<PlayerMainWindow>();
            services.AddSingleton<GmMainWindow>();
            services.AddTransient<TalentInfoWindow>();
            services.AddTransient<CharacterSheetWindow>();

            services.AddTransient<CharacterCreationWindow>();
            services.AddTransient<TalentListWindow>();
            services.AddTransient<FocusListWindow>();

            return services;
        }
    }
}
