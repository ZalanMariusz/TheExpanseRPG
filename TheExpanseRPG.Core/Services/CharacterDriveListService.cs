using System.Data;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterDriveListService : IExpanseService, ICharacterDriveListService
    {
        private ISqliteDatabaseConnectorService DBConnector { get; }
        private ITalentListService TalentListService { get; }
        public List<CharacterDrive> DriveList { get; } = new();
        public Dictionary<string, List<CharacterTalent>> DriveTalentList { get; } = new();
        public List<ICharacterCreationBonus> DriveBonuses { get; } = new();
        public CharacterDriveListService(ISqliteDatabaseConnectorService dbConnector, ITalentListService talentListService)
        {
            DBConnector = dbConnector;
            TalentListService = talentListService;
            PopulateDriveLists(DBConnector.GetDrives());
            PopulateDriveBonuses();
        }
        private void PopulateDriveLists(DataSet driveDataSet)
        {
            DataTable drives = driveDataSet.Tables["Drives"]!;
            DataTable driveTalents = driveDataSet.Tables["DriveTalents"]!;
            foreach (DataRow drive in drives.Rows)
            {
                EnumerableRowCollection professionTalentByDrive = driveTalents.AsEnumerable()
                    .Where(x => x.Field<string>("DriveName") == drive["DriveName"].ToString());

                DriveList.Add(new CharacterDrive(
                    drive["DriveName"].ToString()!,
                    drive["DriveDescription"].ToString()!,
                    drive["DriveQualityDescription"].ToString()!,
                    drive["DriveDownfallDescription"].ToString()!,
                    drive["DriveQuality"].ToString()!,
                    drive["DriveDownfall"].ToString()!
                    ));

                ParseDriveTalents(drive["DriveName"].ToString()!, professionTalentByDrive);
            }
        }
        private void ParseDriveTalents(string driveName, EnumerableRowCollection professionTalentByDrive)
        {
            List<CharacterTalent> driveTalents = new();
            foreach (DataRow talent in professionTalentByDrive)
            {
                driveTalents.Add(TalentListService.GetTalent(talent["TalentName"].ToString()!));
            }
            DriveTalentList.Add(driveName, driveTalents);
        }
        private void PopulateDriveBonuses()
        {
            DriveBonuses.AddRange(
                new List<ICharacterCreationBonus>() {
                    new Fortune() { Value = 5 },
                    new Membership(),
                    new Income(2),
                    new Relationship(),
                    new Reputation()
                    }
                );
        }

        public List<CharacterTalent> GetDriveTalentOptions(string? driveName)
        {
            return DriveTalentList.SingleOrDefault(x => x.Key == driveName).Value ?? new();
        }


    }
}
