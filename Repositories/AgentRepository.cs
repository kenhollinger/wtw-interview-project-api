using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using wtw_interview_project_api.Database;
using wtw_interview_project_api.Entities;

namespace wtw_interview_project_api.Repositories
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAllAgentsAsync();
        Task<Agent> GetAgentAsync(int agentId);
        Task<IEnumerable<License>> GetAllLicensesAsync();
        Task<IEnumerable<AgentLicense>> GetAgentLicensesByAgentIdAsync(int agentId);
        Task<string> UpdateAgentLicenseAsync(AgentLicense agentLicense);
        Task<string> DeleteAgentLicenseAsync(AgentLicense agentLicense);
        string UpdateAgentStatus(int agentId, int newStatus);
    }
    public class AgentRepository : IAgentRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        public AgentRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<IEnumerable<Agent>> GetAllAgentsAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryAsync<Agent>("SELECT * FROM Agent");
        }

        public async Task<Agent> GetAgentAsync(int agentId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryFirstOrDefaultAsync<Agent>($"SELECT * FROM Agent WHERE AgentId={agentId}");
        }
        public async Task<IEnumerable<License>> GetAllLicensesAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryAsync<License>("SELECT * FROM License");
        }
        public async Task<IEnumerable<AgentLicense>> GetAgentLicensesByAgentIdAsync(int agentId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryAsync<AgentLicense>($"SELECT * FROM AgentLicense WHERE AgentId={agentId}");
        }
        public async Task<string> UpdateAgentLicenseAsync(AgentLicense agentLicense)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var response = await connection.QueryFirstOrDefaultAsync<AgentLicense>($"SELECT * FROM AgentLicense WHERE AgentId={agentLicense.AgentId} AND LicenseId={agentLicense.LicenseId}");
            if (response == null) connection.Execute($@"INSERT INTO AgentLicense (AgentId, LicenseId) VALUES ({agentLicense.AgentId}, {agentLicense.LicenseId})"); 
            return "Success";
        }
        public async Task<string> DeleteAgentLicenseAsync(AgentLicense agentLicense)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var response = await connection.QueryFirstOrDefaultAsync<AgentLicense>($"SELECT * FROM AgentLicense WHERE AgentId={agentLicense.AgentId} AND LicenseId={agentLicense.LicenseId}");
            if (response != null) connection.Execute($@"DELETE FROM AgentLicense WHERE AgentId={agentLicense.AgentId} AND LicenseId={agentLicense.LicenseId}");
            return "Success";
        }
        public string UpdateAgentStatus(int agentId,int newStatus)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            connection.Execute($@"UPDATE Agent Set Status={newStatus} WHERE AgentId={agentId} ");
            return "Success";
        }
    }
}
