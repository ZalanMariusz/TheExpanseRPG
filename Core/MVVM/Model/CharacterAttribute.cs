using System.Collections.Generic;
using System.Windows.Documents;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class CharacterAttribute
    {
        public int? BaseValue { get; private set; }
        public int? Modifier { get; set; } = 0;
        public int? AttributeValue { get => BaseValue + Modifier; }
        public CharacterAttributeName AttributeName {  get; set; }
        public List<object> Focuses { get; set; }
        public CharacterAttribute(CharacterAttributeName attributeName,int baseValue)
        {
            BaseValue = baseValue;
            AttributeName = attributeName;
            Focuses = new List<object>();
        }
        
    }
}
