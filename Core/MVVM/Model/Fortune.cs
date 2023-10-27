using TheExpanseRPG.Core.MVVM.Model.Interfaces;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class Fortune : IDriveBonus
    {
        public int Value { get; set; }

        public string DriveBonusName => "+5 Fortune";
    }
}
