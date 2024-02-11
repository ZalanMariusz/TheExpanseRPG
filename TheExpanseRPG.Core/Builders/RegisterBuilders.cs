using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.Builders.Interfaces;

namespace TheExpanseRPG.Core.Builders;

public static class RegisterBuilders
{
    public static IServiceCollection RegisterCoreBuilders(this IServiceCollection services)
    {
        services.AddTransient<ICharacterOriginBuilder, CharacterOriginBuilder>();
        services.AddTransient<ICharacterSocialAndBackgroundBuilder, CharacterSocialAndBackgroundBuilder>();
        services.AddTransient<ICharacterProfessionBuilder, CharacterProfessionBuilder>();
        services.AddTransient<ICharacterDriveBuilder, CharacterDriveBuilder>();
        services.AddTransient<ICharacterAbilityBlockBuilder, CharacterAbilityBlockBuilder>();
        return services;
    }
}
