using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model
{
    public static class DummyDataGenerator
    {
        public static CharacterAbility DummyAbility => new(CharacterAbilityName.Accuracy);
        public static AbilityFocus DummyFocus => new(CharacterAbilityName.Accuracy, string.Empty);
        public static CharacterBackGround DummyBackground => new(string.Empty, string.Empty, CharacterSocialClass.Outsider, new CharacterAbility(CharacterAbilityName.Accuracy, 0), new(), new(), new());
        
        public static CharacterProfession DummyOutsiderProfession => new(string.Empty, string.Empty, CharacterSocialClass.Outsider, new(), new());
        public static CharacterProfession DummyLowerProfession => new(string.Empty, string.Empty, CharacterSocialClass.Lower, new(), new());
        public static CharacterProfession DummyMiddleProfession => new(string.Empty, string.Empty, CharacterSocialClass.Middle, new(), new());
        public static CharacterProfession DummyUpperProfession => new(string.Empty, string.Empty, CharacterSocialClass.Upper, new(), new());
        public static CharacterProfession DummyProfession => DummyOutsiderProfession;
        public static CharacterDrive DummyDrive => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        public static CharacterTalent DummyTalent => new(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty);
    }
}
