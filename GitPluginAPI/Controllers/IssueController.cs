namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;
    
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IProjectService _projectService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpPost]
        [Route("{projectId}/issues")]
        public async Task<string> CreateIssue(int projectId, string title, string description)
        {
            return await _issueService.CreateIssue(projectId, title, description);
        }

        [HttpDelete]
        [Route("{projectId}/issues/{issueiid}")]
        public async Task<string> DeleteIssue(int projectId, int issueiid)
        {
            return await _issueService.DeleteIssue(projectId, issueiid);
        }
        
        [HttpGet]
        [Route("{projectId}/issues")]
        public async Task<string> GetProjectIssues(int projectId)
        {
            return await _issueService.GetProjectIssues(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/issues/{issueId}")]
        public async Task<string> GetIssueById(int projectId, int issueId)
        {
            return await _issueService.GetIssueById(projectId, issueId);
        }

        [HttpPut]
        [Route("{projectId}/issues/{issueId}")]
        public async Task<string> UpdateIssueState(int projectId, int issueId, string title, string description)
        {
            return await _issueService.UpdateIssueState(projectId, issueId, title, description);
        }
    }
}