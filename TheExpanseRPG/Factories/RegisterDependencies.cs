using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Factories.Interfaces;
using TheExpanseRPG.Factories.Interfaces;
using TheExpanseRPG.MVVM.ViewModel;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Factories
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterFactories(this IServiceCollection services)
        {
            //var enumDataSource = Enum.GetValues(typeof(CharacterOrigin));

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IViewFactory, ViewFactory>();
            services.AddSingleton<ScopedServiceFactory>();
            
            services.AddSingleton<Func<Type, IViewModelBase>>
                (provider => viewModelType => (IViewModelBase)provider.GetRequiredService(viewModelType));

            services.AddSingleton<Func<Type, Window>>(
                provider => windowType => (Window)provider.GetRequiredService(windowType));

            return services;
        }
    }
}
