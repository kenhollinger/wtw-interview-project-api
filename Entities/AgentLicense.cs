namespace wtw_interview_project_api.Entities
{
    public class AgentLicense
    {
        public AgentLicense() {}
        public AgentLicense(int agentId, int licenseId)
        {
            AgentId = agentId;
            LicenseId = licenseId;
        }
        public int AgentId { get; set; }
        public int LicenseId { get; set; }
    }
}
