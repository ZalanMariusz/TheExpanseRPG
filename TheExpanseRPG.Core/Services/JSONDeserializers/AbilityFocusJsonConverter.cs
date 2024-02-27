using System.Text.Json;
using System.Text.Json.Serialization;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services.JSONDeserializers;

public class AbilityFocusJsonConverter : JsonConverter<List<AbilityFocus>>
{
    IAbilityFocusListService FocusListService { get; set; }
    public AbilityFocusJsonConverter(IAbilityFocusListService focusListService)
    {
        FocusListService = focusListService;
    }
    public override List<AbilityFocus> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<AbilityFocus> retval = new();

        CharacterAbilityName abilityName = 0;
        string focusName = string.Empty;
        bool isImproved = false;
        while (reader.TokenType != JsonTokenType.EndArray)
        {
            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                AbilityFocus focus = (AbilityFocus)FocusListService.GetFocusByName(abilityName, focusName).ShallowCopy();
                if (isImproved)
                {
                    focus.ImproveFocus();
                }
                retval.Add(focus);
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propName = reader.GetString()!.ToLower();
                reader.Read();

                switch (propName)
                {
                    case var _ when propName.Equals(nameof(AbilityFocus.AbilityName).ToLower()):
                        abilityName = (CharacterAbilityName)reader.GetInt32();
                        break;
                    case var _ when propName.Equals(nameof(AbilityFocus.FocusName).ToLower()):
                        focusName = reader.GetString()!;
                        break;
                    case var _ when propName.Equals(nameof(AbilityFocus.IsImproved).ToLower()):
                        isImproved = reader.GetBoolean();
                        break;
                }
            }
        }

        return retval;
    }

    public override void Write(Utf8JsonWriter writer, List<AbilityFocus> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
