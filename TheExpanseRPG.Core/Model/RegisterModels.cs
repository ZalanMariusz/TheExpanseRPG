using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public static class RegisterModels
{
    public static IServiceCollection RegisterCoreModels(this IServiceCollection services)
    {
        services.AddTransient<ExpanseCharacter>();
        services.AddTransient<IRandomGenerator, RandomGenerator>();
        return services;
    }
}
