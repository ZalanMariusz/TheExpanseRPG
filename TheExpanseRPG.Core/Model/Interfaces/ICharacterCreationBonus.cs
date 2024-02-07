using System.Text.Json.Serialization;

namespace TheExpanseRPG.Core.Model.Interfaces
{
    public interface ICharacterCreationBonus
    {
        [JsonIgnore]
        public string CreationBonusName { get; }
        public ICharacterCreationBonus ShallowCopy();
    }
}
