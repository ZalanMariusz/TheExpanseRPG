using Microsoft.Extensions.DependencyInjection;

namespace TheExpanseRPG.Core.MVVM.View
{
    public static class RegisterViews
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<WelcomeSplashWindow>();
            services.AddSingleton<PlayerMainWindow>();
            services.AddSingleton<GmMainWindow>();

            services.AddTransient<CharacterCreationWindow>();
            //services.AddTransient<OriginSelectView>();
            //services.AddTransient<AttributeRollView>();
            services.AddTransient<SocialAndBackgroundView>();
            services.AddTransient<CharacterProfessionView>();
            services.AddTransient<DriveAndGoalsView>();

        }
    }
}
