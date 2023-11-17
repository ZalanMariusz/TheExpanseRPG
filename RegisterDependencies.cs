using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Factories;
using TheExpanseRPG.MVVM.ViewModel;
using TheExpanseRPG.Services;

namespace TheExpanseRPG
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterMVVMDependencies(this IServiceCollection services)
        {
            services.RegisterViewModels();
            services.RegisterViews();
            services.RegisterFactories();
            services.RegisterServices();
            return services;
        }
    }
}
