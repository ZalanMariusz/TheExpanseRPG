using System.Text.Json;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services.Interfaces;
using TheExpanseRPG.Core.Services.JSONDeserializers;

namespace TheExpanseRPG.Core.Services;

public class CharacterListService : ICharacterListService
{

    private List<ExpanseCharacter> CharacterList { get; } = new();
    private IAbilityFocusListService FocusListService { get; }
    private ITalentListService TalentListService { get; }
    public CharacterListService(IAbilityFocusListService focusListService, ITalentListService talentListService)
    {
        FocusListService = focusListService;
        TalentListService = talentListService;
    }

    private void DeserializeCharacters(IAbilityFocusListService focusListService, ITalentListService talentListService)
    {
        CharacterList.Clear();
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new AbilityFocusJsonConverter(focusListService),
                new CharacterTalentJsonConverter(talentListService),
                new CharacterAbilityJsonConverter()
            }
        };
        string characterFolderPath = ModelResources.CharacterSavePath;
        string[] charJsonPaths = Directory.GetFiles(characterFolderPath, "*.json");

        foreach (string expanseCharacterPath in charJsonPaths)
        {
            string charJson = File.ReadAllText(expanseCharacterPath);

            ExpanseCharacter character = JsonSerializer.Deserialize<ExpanseCharacter>(charJson, options)!;
            CharacterList.Add(character);
        }
    }

    public List<ExpanseCharacter> GetCharacterList()
    {
        DeserializeCharacters(FocusListService, TalentListService);
        return CharacterList;
    }
}
