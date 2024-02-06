using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Core;
using TheExpanseRPG.MVVM;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG
{
    
    public partial class App : Application
    {
        public static bool IsNavigating { get; set; } = false;
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            _services = new ServiceCollection();
            
            _services.RegisterCoreDepdendencies();
            _services.RegisterMVVMDependencies();

            _serviceProvider = _services.BuildServiceProvider();
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            //EventAggregator_ToDelete.InitPropertiesToLink();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService navigationService = _serviceProvider.GetRequiredService<INavigationService>();
            navigationService.NavigateToNewWindow<WelcomeSplashWindow>();
            base.OnStartup(e);
        }
        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var t = new StackTrace();
            t.GetFrames();
            string a = "";
            foreach (var item in t.GetFrames())
            {
                a += "\n" + item?.GetMethod()?.Name;
            }
            MessageBox.Show($"Unhandled exception occurred: {a} \n" + e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
