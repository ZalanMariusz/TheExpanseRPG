using System.Data;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface ISqliteDatabaseConnectorService
    {
        DataTable GetAbilityFocuses();
        DataSet GetBackgrounds();
        DataSet GetDrives();
        DataSet GetProfessions();
        DataTable GetTalentRequirements();
        DataTable GetTalents();
        Task<DataTable> GetTalentsAsync();
        Task<DataTable> GetTalentRequirementsAsync();

    }
}