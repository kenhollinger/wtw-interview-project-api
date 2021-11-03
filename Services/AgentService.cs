using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using wtw_interview_project_api.Entities;
using wtw_interview_project_api.Repositories;

namespace wtw_interview_project_api.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> GetAllAgentsAsync();
        Task<AgentDetail> GetAgentDetailAsync(int agentId);
        Task<string> UpdateAgentDetailAsync(AgentDetail agentDetail);
    }
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;
        public AgentService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }
        public async Task<IEnumerable<Agent>> GetAllAgentsAsync() => await _agentRepository.GetAllAgentsAsync();

        public async Task<AgentDetail> GetAgentDetailAsync(int agentId)
        {
            var agentDetail = new AgentDetail(await _agentRepository.GetAgentAsync(agentId));

            var licenses = await _agentRepository.GetAllLicensesAsync();
            foreach (var license in licenses)
            {
                agentDetail.Licenses.Add(new LicenseDetail() { LicenseId = license.LicenseId, LicenseName = license.LicenseName, IsRequired = license.IsRequired, Selected = false });
            }

            var agentLicenses = await _agentRepository.GetAgentLicensesByAgentIdAsync(agentId);
            foreach (var agentLicense in agentLicenses)
            {
                agentDetail.Licenses.FirstOrDefault(x => x.LicenseId == agentLicense.LicenseId).Selected = true;
            }
            return agentDetail;
        }
        public async Task<string> UpdateAgentDetailAsync(AgentDetail agentDetail)
        {
            foreach (var license in agentDetail.Licenses)
            {
                if(license.Selected) await _agentRepository.UpdateAgentLicenseAsync(new AgentLicense(agentDetail.AgentId, license.LicenseId));
                else await _agentRepository.DeleteAgentLicenseAsync(new AgentLicense(agentDetail.AgentId, license.LicenseId));
            }
            return UpdateStatus(agentDetail);
        }
        private string UpdateStatus(AgentDetail agentDetail)
        {
            var anyRequiredSelected = agentDetail.Licenses.Any(l => l.IsRequired && l.Selected);
            var anyRequiredAndNotSelected = agentDetail.Licenses.Any(l => l.IsRequired && !l.Selected);
            var newStatus = 3;
            if(anyRequiredSelected && !anyRequiredAndNotSelected) newStatus = 1;
            if(anyRequiredSelected && anyRequiredAndNotSelected) newStatus = 2;
            return _agentRepository.UpdateAgentStatus(agentDetail.AgentId, newStatus);
        }
    }
}
