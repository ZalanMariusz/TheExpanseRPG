using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IDiceRollService, DiceRollService>();
            services.AddSingleton<ITalentListService, TalentListService>();
            services.AddSingleton<ICharacterBackgroundListService, CharacterBackgroundListService>();
            services.AddSingleton<IAbilityFocusListService, AbilityFocusListService>();
            services.AddSingleton<ICharacterProfessionListService, CharacterProfessionListService>();
            services.AddSingleton<ICharacterDriveListService, CharacterDriveListService>();
            services.AddSingleton<ISqliteDatabaseConnectorService, SqliteDatabaseConnectorService>();
            services.AddScoped<ICharacterCreationService, CharacterCreationService>();
            services.AddSingleton<ICharacterListService, CharacterListService>();
            return services;
        }
    }
}
