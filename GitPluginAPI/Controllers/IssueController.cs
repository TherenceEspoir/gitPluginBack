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
        [Route("{projectId}/issues/{issueId}")]
        public async Task<string> DeleteIssue(int projectId, int issueIid)
        {
            return await _projectService.DeleteIssue(projectId, issueIid);
        }

        [HttpGet]
        [Route("{projectId}/issues")]
        public async Task<string> GetProjectIssues(int projectId)
        {
            return await _projectService.GetAllProjectIssues(projectId);
        }

        [HttpPut]
        [Route("{projectId}/issues/{issueId}")]
        public async Task<string> UpdateIssueState(int projectId, int issueId, string state_event)
        {
            return await _projectService.UpdateIssueState(projectId, issueId, state_event);
        }
    }
}