using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using wtw_interview_project_api.Entities;
using wtw_interview_project_api.Services;

namespace wtw_interview_project_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<AgentController> _logger;
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService, ILogger<AgentController> logger)
        {
            _logger = logger;
            _agentService = agentService;
        }

        [Route("/Ping"), HttpGet]
        public string Ping() => "Pong";

        [Route("/GetAllAgents"), HttpGet]
        public async Task<IEnumerable<Agent>> GetAllAgents() => await _agentService.GetAllAgentsAsync();

        [Route("/GetAgentDetail"), HttpGet]
        public async Task<AgentDetail> GetAgentDetail(int agentId) => await _agentService.GetAgentDetailAsync(agentId);

        [Route("/UpdateAgentDetail"), HttpPost]
        public async Task<string> UpdateAgentDetail(AgentDetail agentDetail) => await _agentService.UpdateAgentDetailAsync(agentDetail);
    }
}
