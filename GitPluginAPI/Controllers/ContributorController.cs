namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;
    
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ContributorController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("{projectId}/contributors")]
        public async Task<string> GetProjectContributors(int projectId)
        {
            return await _projectService.GetAllContributors(projectId);
        }
    }
}