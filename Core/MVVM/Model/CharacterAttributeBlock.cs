using System;
using System.Collections.Generic;
using System.Linq;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class CharacterAttributeBlock
    {
        //public CharacterAttribute Accuracy { get; }
        //public CharacterAttribute Constitution { get; }
        //public CharacterAttribute Fighting { get; private set; }
        //public CharacterAttribute Communication { get; }
        //public CharacterAttribute Dexterity { get; }
        //public CharacterAttribute Intelligence { get; }
        //public CharacterAttribute Perception { get; }
        //public CharacterAttribute Strength { get; }
        //public CharacterAttribute Willpower { get; }
        public List<CharacterAttribute> AttributeBlock { get; private set; }
        public CharacterAttributeBlock(List<Tuple<CharacterAttributeName, int>> attributeTupleList)
        {
            AttributeBlock = new List<CharacterAttribute>(CreateAttributes(attributeTupleList));
        }

        public CharacterAttribute GetAttribute(CharacterAttributeName attributeName)
        {
            CharacterAttribute characterAttribute = AttributeBlock.FirstOrDefault(a => a.AttributeName == attributeName)!;
            return characterAttribute ?? throw (new KeyNotFoundException());
        }

        private static List<CharacterAttribute> CreateAttributes(List<Tuple <CharacterAttributeName, int>> attributeTupleList)
        {
            List<CharacterAttribute> retAttributes = new();
            foreach (var tupleItem in attributeTupleList)
            {
                retAttributes.Add(new CharacterAttribute(tupleItem.Item1, tupleItem.Item2));
            }
            return retAttributes;
        }
        public CharacterAttribute GetStrength()
        {
            return GetAttribute(CharacterAttributeName.Strength);
        }
        public CharacterAttribute GetConstitution()
        {
            return GetAttribute(CharacterAttributeName.Constitution);
        }
        public CharacterAttribute GetAccuracy()
        {
            return GetAttribute(CharacterAttributeName.Accuracy);
        }
        public CharacterAttribute GetIntelligence()
        {
            return GetAttribute(CharacterAttributeName.Intelligence);
        }
        public CharacterAttribute GetPerception()
        {
            return GetAttribute(CharacterAttributeName.Perception);
        }
        public CharacterAttribute GetCommunication()
        {
            return GetAttribute(CharacterAttributeName.Communication);
        }
        public CharacterAttribute GetDexterity()
        {
            return GetAttribute(CharacterAttributeName.Dexterity);
        }
        public CharacterAttribute GetFighting()
        {
            return GetAttribute(CharacterAttributeName.Fighting);
        }
        public CharacterAttribute GetWillpower()
        {
            return GetAttribute(CharacterAttributeName.Willpower);
        }

    }
}
