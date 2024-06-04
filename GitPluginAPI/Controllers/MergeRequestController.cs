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
    }
}