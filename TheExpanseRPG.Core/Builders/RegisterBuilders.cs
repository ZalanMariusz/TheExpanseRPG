using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.Builders.Interfaces;

namespace TheExpanseRPG.Core.Builders;

public static class RegisterBuilders
{
    public static IServiceCollection RegisterCoreBuilders(this IServiceCollection services)
    {
        services.AddTransient<ICharacterAbilityBlockBuilder, CharacterAbilityBlockBuilder>();
        return services;
    }
}
