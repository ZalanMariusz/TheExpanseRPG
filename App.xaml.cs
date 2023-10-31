using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Core.MVVM.ViewModel;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG
{
    public partial class App : Application
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            _services = new ServiceCollection();

            RegisterModels.Register(_services);
            RegisterViewModels.Register(_services);
            RegisterViews.Register(_services);
            RegisterFactories.Register(_services);
            RegisterServices.Register(_services);
            _serviceProvider = _services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService navigationService = _serviceProvider.GetRequiredService<INavigationService>();
            navigationService.NavigateToNewWindow<WelcomeSplashWindow>();
            base.OnStartup(e);
        }
    }
}
