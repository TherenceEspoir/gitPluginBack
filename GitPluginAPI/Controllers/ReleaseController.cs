namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;
    
    [ApiController]
    public class ReleaseController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ReleaseController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("{projectId}/releases")]
        public async Task<string> GetProjectReleases(int projectId)
        {
            return await _projectService.GetAllProjectReleases(projectId);
        }
    }
}