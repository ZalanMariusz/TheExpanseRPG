namespace TheExpanseRPG.Core.Model.Interfaces
{
    public interface ICharacterCreationBonus
    {
        public string CreationBonusName { get; }
        public ICharacterCreationBonus ShallowCopy();
    }
}
