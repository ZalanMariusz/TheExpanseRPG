using Microsoft.Data.Sqlite;
using System.Data;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services;

public class SqliteDatabaseConnectorService : ISqliteDatabaseConnectorService
{
    private const string SqliteConnectionString = "Data Source=TheExpanseRPGDB.db;";

    private const string TALENTQUERY = "SELECT TalentName,Description,NoviceDescription,ExpertDescription,MasterDescription FROM Talent";
    private const string TALENTREQUIREMENTQUERY = "SELECT TalentName,RequirementString FROM TalentRequirement";
    private const string ABILITYFOCUSQUERY = "SELECT AbilityId,FocusName,FocusDescription FROM AbilityFocus";
    private const string BACKGROUNDQUERY = "SELECT BackgroundName, BackgroundDescription, MainSocialClass, AbilityBonus FROM Background";
    private const string BACKGROUNDFOCUSQUERY = "SELECT BackgroundName, AbilityId, FocusName FROM BackgroundFocus";
    private const string BACKGROUNDTALENTQUERY = "SELECT BackgroundName, TalentName FROM BackgroundTalent";
    private const string BACKGROUNDBENEFITQUERY = "SELECT BackgroundName, BenefitTypeFlag, BenefitString FROM BackgroundBenefit";
    private const string PROFESSIONQUERY = "SELECT ProfessionName,ProfessionDescription,SocialClassId FROM Profession";
    private const string PROFESSIONFOCUSQUERY = "SELECT ProfessionName,AbilityId,FocusName FROM ProfessionFocus";
    private const string PROFESSIONTALENTQUERY = "SELECT ProfessionName,TalentName FROM ProfessionTalent";
    private const string DRIVEQUERY = "SELECT DriveName,DriveDescription,DriveQuality,DriveDownfall,DriveQualityDescription,DriveDownfallDescription FROM Drive";
    private const string DRIVETALENTQUERY = "SELECT DriveName,TalentName FROM DriveTalent";

    private SqliteConnection Connection { get; }
    public SqliteDatabaseConnectorService()
    {
        Connection = CreateConnection();
    }
    private static SqliteConnection CreateConnection()
    {

        SqliteConnection sqlite_conn;
        sqlite_conn = new SqliteConnection(SqliteConnectionString);
        sqlite_conn.Open();
        return sqlite_conn;
    }

    public DataTable GetAbilityFocuses()
    {
        return ExecuteSelectQuery(ABILITYFOCUSQUERY);
    }
    public DataTable GetTalents()
    {
        return ExecuteSelectQuery(TALENTQUERY);
    }
    public DataTable GetTalentRequirements()
    {
        return ExecuteSelectQuery(TALENTREQUIREMENTQUERY);
    }

    public DataSet GetBackgrounds()
    {
        DataSet retval = new();
        DataTable backgrounds = ExecuteSelectQuery(BACKGROUNDQUERY);
        DataTable backgroundFocuses = ExecuteSelectQuery(BACKGROUNDFOCUSQUERY);
        DataTable backgroundTalents = ExecuteSelectQuery(BACKGROUNDTALENTQUERY);
        DataTable backgroundBenefits = ExecuteSelectQuery(BACKGROUNDBENEFITQUERY);

        backgrounds.TableName = "Backgrounds";
        backgroundFocuses.TableName = "BackgroundFocuses";
        backgroundTalents.TableName = "BackgroundTalents";
        backgroundBenefits.TableName = "BackgroundBenefits";

        retval.Tables.Add(backgrounds);
        retval.Tables.Add(backgroundFocuses);
        retval.Tables.Add(backgroundTalents);
        retval.Tables.Add(backgroundBenefits);

        return retval;
    }

    public DataSet GetProfessions()
    {
        DataSet retval = new();
        DataTable professions = ExecuteSelectQuery(PROFESSIONQUERY);
        DataTable professionFocuses = ExecuteSelectQuery(PROFESSIONFOCUSQUERY);
        DataTable professionTalents = ExecuteSelectQuery(PROFESSIONTALENTQUERY);

        professions.TableName = "Professions";
        professionFocuses.TableName = "ProfessionFocuses";
        professionTalents.TableName = "ProfessionTalents";

        retval.Tables.Add(professions);
        retval.Tables.Add(professionFocuses);
        retval.Tables.Add(professionTalents);

        return retval;
    }
    public DataSet GetDrives()
    {
        DataSet retval = new();
        DataTable drives = ExecuteSelectQuery(DRIVEQUERY);
        DataTable driveTalents = ExecuteSelectQuery(DRIVETALENTQUERY);

        drives.TableName = "Drives";
        driveTalents.TableName = "DriveTalents";

        retval.Tables.Add(drives);
        retval.Tables.Add(driveTalents);

        return retval;
    }
    private DataTable ExecuteSelectQuery(string query)
    {
        DataTable dt = new();
        SqliteCommand sqlite_cmd = Connection.CreateCommand();
        sqlite_cmd.CommandText = query;
        dt.Load(sqlite_cmd.ExecuteReader());
        return dt;
    }

    private async Task<DataTable> ExecuteSelectQueryAsync(string query)
    {
        DataTable dt = new();
        SqliteCommand sqlite_cmd = Connection.CreateCommand();
        sqlite_cmd.CommandText = query;
        dt.Load(await sqlite_cmd.ExecuteReaderAsync());
        return dt;
    }

    public async Task<DataTable> GetTalentsAsync()
    {
        return await ExecuteSelectQueryAsync(TALENTQUERY);
    }

    public async Task<DataTable> GetTalentRequirementsAsync()
    {
        return await ExecuteSelectQueryAsync(TALENTREQUIREMENTQUERY);
    }
}
