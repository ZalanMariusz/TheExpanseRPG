using Microsoft.Extensions.DependencyInjection;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<WelcomeSplashViewModel>();
            services.AddTransient<PlayerMainViewModel>();
            services.AddSingleton<GmMainViewModel>();
            
            services.AddTransient<TalentInfoViewModel>();
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
            services.AddTransient<FocusListViewModel>();
            services.AddTransient<PopupViewModelBase>();
            services.AddTransient<CharacterFinalizationViewModel>();
            services.AddTransient<CharacterSheetViewModel>();
            services.AddTransient<CharacterDetailsViewModel>();
            return services;
        }
    }
}
