using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TheExpanseRPG.Core;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG
{
    public partial class App : Application
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            _services = new ServiceCollection();
            _services.RegisterCoreDepdendencies();
            _services.RegisterMVVMDependencies();
            //RegisterServices.Register(_services);
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
