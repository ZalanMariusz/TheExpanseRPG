using System.Text.Json;
using System.Text.Json.Serialization;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services.JSONDeserializers;

public class CharacterTalentJsonConverter : JsonConverter<List<CharacterTalent>>
{
    ITalentListService TalentListService { get; set; }
    public CharacterTalentJsonConverter(ITalentListService focusListService)
    {
        TalentListService = focusListService;
    }
    public override List<CharacterTalent> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<CharacterTalent> retval = new();

        int degree = 0;
        string talentName = string.Empty;

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                CharacterTalent talent = (CharacterTalent)TalentListService.GetTalent(talentName).ShallowCopy();
                while (talent.Degree != (TalentDegree)degree)
                {
                    talent.ImproveTalent();
                }
                retval.Add(talent);
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propName = reader.GetString()!.ToLower();
                reader.Read();

                switch (propName)
                {
                    case var _ when propName.Equals(nameof(CharacterTalent.TalentName).ToLower()):
                        talentName = reader.GetString()!;
                        break;
                    case var _ when propName.Equals(nameof(CharacterTalent.Degree).ToLower()):
                        degree = reader.GetInt32();
                        break;
                }
            }
        }

        return retval;
    }
    public override void Write(Utf8JsonWriter writer, List<CharacterTalent> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
