using System.Data.SQLite;
using System;
using System.Data;
using System.Windows.Controls.Primitives;

namespace TheExpanseRPG.Core.Services
{
    public class SqliteDatabaseConnectorService
    {
        private const string TALENTQUERY = "SELECT TalentName,Description,NoviceDescription,ExpertDescription,MasterDescription FROM Talent";
        private const string TALENTREQUIREMENTQUERY = "SELECT TalentName,RequirementString FROM TalentRequirement";
        private const string ABILITYFOCUSQUERY = "SELECT AbilityId,FocusName FROM AbilityFocus";
        private const string BACKGROUNDQUERY = "SELECT BackgroundName, BackgroundDescription, MainSocialClass, AbilityBonus FROM Background";
        private const string BACKGROUNDFOCUSQUERY = "SELECT BackgroundName, AbilityId, FocusName FROM BackgroundFocus";
        private const string BACKGROUNDTALENTQUERY = "SELECT BackgroundName, TalentName FROM BackgroundTalent";
        private const string BACKGROUNDBENEFITQUERY = "SELECT BackgroundName, BenefitTypeFlag, BenefitString FROM BackgroundBenefit";

        private const string PROFESSIONQUERY = "SELECT ProfessionName,ProfessionDescription,SocialClassId FROM Profession";
        private const string PROFESSIOFOCUSQUERY = "SELECT ProfessionName,AbilityId,FocusName FROM ProfessionFocus";
        private const string PROFESSIOTALENTQUERY = "SELECT ProfessionName,TalentName FROM ProfessionTalent";

        private SQLiteConnection Connection { get; }
        public SqliteDatabaseConnectorService()
        {
            Connection = CreateConnection();
        }
        private static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=TheExpanseRPGDB.db; Version = 3; New = True; Compress = True; ");
            sqlite_conn.Open();
            return sqlite_conn;
        }

        public DataTable GetAbilityFocuses()
        {
            return ExecuteQuery(ABILITYFOCUSQUERY);
        }
        public DataTable GetTalents()
        {
            return ExecuteQuery(TALENTQUERY);
        }
        public DataTable GetTalentRequirements()
        {
            return ExecuteQuery(TALENTREQUIREMENTQUERY);
        }

        public DataSet GetBackgrounds()
        {
            DataSet retval = new();
            DataTable backgrounds = ExecuteQuery(BACKGROUNDQUERY);
            DataTable backgroundFocuses = ExecuteQuery(BACKGROUNDFOCUSQUERY);
            DataTable backgroundTalents = ExecuteQuery(BACKGROUNDTALENTQUERY);
            DataTable backgroundBenefits = ExecuteQuery(BACKGROUNDBENEFITQUERY);

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
            DataTable professions = ExecuteQuery(PROFESSIONQUERY);
            DataTable professionFocuses = ExecuteQuery(PROFESSIOFOCUSQUERY);
            DataTable professionTalents = ExecuteQuery(PROFESSIOTALENTQUERY);

            professions.TableName = "Professions";
            professionFocuses.TableName = "ProfessionFocuses";
            professionTalents.TableName = "ProfessionTalents";

            retval.Tables.Add(professions);
            retval.Tables.Add(professionFocuses);
            retval.Tables.Add(professionTalents);

            return retval;
        }


        private DataTable ExecuteQuery(string query)
        {
            //if (Connection.State == ConnectionState.Closed)
            //{
            //    Connection.Open();
            //}
            DataTable dt = new();
            SQLiteCommand sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = query;
            dt.Load(sqlite_cmd.ExecuteReader());
            //Connection.Close();
            return dt;
        }
    }
}
