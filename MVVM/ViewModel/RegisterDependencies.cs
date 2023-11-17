using Microsoft.Extensions.DependencyInjection;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<WelcomeSplashViewModel>();
            services.AddSingleton<PlayerMainViewModel>();
            services.AddSingleton<GmMainViewModel>();

            services.AddTransient<CharacterCreationViewModelBase>();
            services.AddTransient<CharacterCreationViewModel>();
            services.AddTransient<OriginSelectViewModel>();
            services.AddTransient<AbilityRollViewModel>();
            services.AddTransient<SocialAndBackgroundViewModel>();
            services.AddTransient<CharacterProfessionViewModel>();
            services.AddTransient<DrivesViewModel>();
            services.AddTransient<AllRandomAbilityRollViewModel>();
            services.AddTransient<DistributeAbilitiesViewModel>();
            services.AddTransient<AssignAbilityRollViewModel>();
            services.AddTransient<TalentListViewModel>();
            return services;
        }
    }
}
