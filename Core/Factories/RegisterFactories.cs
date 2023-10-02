using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System;
using TheExpanseRPG.Core.Factories.Interfaces;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using System.Security.Principal;
using System.CodeDom;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Factories
{
    public static class RegisterFactories
    {
        public static void Register(IServiceCollection services)
        {

            var enumDataSource = Enum.GetValues(typeof(CharacterOrigin));
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IViewFactory, ViewFactory>();

            //services.AddSingleton<Func<IEnumerable<IViewModelBase>>>
            //  (provider => provider.GetRequiredService<IEnumerable<IViewModelBase>>);


            services.AddSingleton<Func<Type, IViewModelBase>>
                (provider => viewModelType => (IViewModelBase)provider.GetRequiredService(viewModelType));

            services.AddSingleton<Func<Type, Window>>(
                provider => windowType => (Window)provider.GetRequiredService(windowType));
        }
    }
}
