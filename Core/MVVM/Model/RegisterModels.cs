using Microsoft.Extensions.DependencyInjection;

namespace TheExpanseRPG.Core.MVVM.Model
{
    static class RegisterModels
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ExpanseCharacter>();
        }
    }
}
