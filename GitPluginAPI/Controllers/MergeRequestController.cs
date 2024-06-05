namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;

    [Route("api/[controller]")]
    [ApiController]
    public class MergeRequestController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public MergeRequestController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("{projectId}/merge_request")]
        public async Task<string> GetProjectMergeRequest(int projectId)
        {
            return await _projectService.GetAllProjectMergeRequest(projectId);
        }

        [HttpPost]
        [Route("{projectId}/merge_request")]
        public async Task<string> CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title, string description)
        {
            return await _projectService.CreateMergeRequest(projectId, sourceBranch, targetBranch, title, description);
        }

        [HttpDelete]
        [Route("{projectId}/merge_request/{mergeRequestId}")]
        public async Task<string> DeleteMergeRequest(int projectId, int mergeRequestId)
        {
            return await _projectService.DeleteMergeRequest(projectId, mergeRequestId);
        }

        [HttpPut]
        [Route("{projectId}/merge_request/{mergeRequestId}")]
        public async Task<string> UpdateMergeRequest(int projectId, int mergeRequestId, string title, string description)
        {
            return await _projectService.UpdateMergeRequest(projectId, mergeRequestId, title, description);
        }
    }
}