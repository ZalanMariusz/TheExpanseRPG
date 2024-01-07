namespace TheExpanseRPG.Core.Model
{
    public class CharacterDrive
    {
        public string DriveName { get; }
        public string DriveDescription { get; }
        public string QualityDescription { get; }
        public string DownfallDescription { get; }
        public string Quality { get; }
        public string Downfall { get; }
        public CharacterDrive(
            string driveName,
            string driveDescription,
            string driveQualityDescription,
            string driveDownfallDescription,
            string driveQuality,
            string driveDownfall
            )
        {
            DriveName = driveName;
            DriveDescription = driveDescription;
            QualityDescription = driveQualityDescription;
            DownfallDescription = driveDownfallDescription;
            Quality = driveQuality;
            Downfall = driveDownfall;
        }


    }
}
