﻿using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    static class RegisterViewModels
    {
        public static void Register(IServiceCollection services)
        {
     

            services.AddSingleton<WelcomeSplashViewModel>();
            services.AddSingleton<PlayerMainViewModel>();
            services.AddSingleton<GmMainViewModel>();

            services.AddTransient<CharacterCreationViewModel>();
            services.AddTransient<OriginSelectViewModel>();
            services.AddTransient<AttributeRollViewModel>();
            services.AddTransient<SocialAndBackgroundViewModel>();
            services.AddTransient<CharacterProfessionViewModel>();
            services.AddTransient<DriveAndGoalsViewModel>();
            services.AddTransient<AllRandomAttributeRollViewModel>();
            services.AddTransient<DistributeAttributesViewModel>();
            services.AddTransient<AssignAttributeRollViewModel>();
            






        }
    }
}
