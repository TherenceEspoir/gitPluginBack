namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;
    
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public IssueController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        [Route("{projectId}/issues")]
        public async Task<string> CreateIssue(int projectId, string title, string description)
        {
            return await _projectService.CreateIssue(projectId, title, description);
        }

        [HttpDelete]
        [Route("{projectId}/issues/{issueiid}")]
        public async Task<string> DeleteIssue(int projectId, int issueiid)
        {
            return await _projectService.DeleteIssue(projectId, issueiid);
        }
        
        [HttpGet]
        [Route("{projectId}/issues")]
        public async Task<string> GetProjectIssues(int projectId)
        {
            return await _projectService.GetAllProjectIssues(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/issues/{issueId}")]
        public async Task<string> GetIssueById(int projectId, int issueId)
        {
            return await _projectService.GetIssueById(projectId, issueId);
        }

        [HttpPut]
        [Route("{projectId}/issues/{issueId}")]
        public async Task<string> UpdateIssueState(int projectId, int issueId, string title, string description)
        {
            return await _projectService.UpdateIssueState(projectId, issueId, title, description);
        }
    }
}