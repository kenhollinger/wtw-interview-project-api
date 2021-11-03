using System.Collections;
using System.Collections.Generic;

namespace wtw_interview_project_api.Entities
{
    public class AgentDetail
    {
        public AgentDetail() {}
        public AgentDetail(Agent agent) 
        {
            AgentId = agent.AgentId;
            FirstName = agent.FirstName;
            LastName = agent.LastName;
            Status = agent.Status;
            Licenses = new List<LicenseDetail>();
        }
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AgentStatusEnum Status { get; set; }
        public List<LicenseDetail> Licenses { get; set; }
    }
}
