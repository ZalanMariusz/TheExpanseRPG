using System.Text.Json;
using System.Text.Json.Serialization;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Services.JSONDeserializers;

public class CharacterAbilityJsonConverter : JsonConverter<List<CharacterAbility>>
{
    public override List<CharacterAbility>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        int baseScore = 0;
        CharacterAbilityName abilityName = CharacterAbilityName.Accuracy;
        List<CharacterAbility> retval = new();

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                retval.Add(new(abilityName, baseScore));
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propName = reader.GetString()!.ToLower();
                reader.Read();

                switch (propName)
                {
                    case var _ when propName.Equals(nameof(CharacterAbility.AbilityName).ToLower()):
                        abilityName = (CharacterAbilityName)reader.GetInt32();
                        break;
                    case var _ when propName.Equals(nameof(CharacterAbility.BaseValue).ToLower()):
                        baseScore = reader.GetInt32();
                        break;
                }
            }
        }
        return retval;
    }

    public override void Write(Utf8JsonWriter writer, List<CharacterAbility> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
