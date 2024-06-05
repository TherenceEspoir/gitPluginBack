namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;
    
    [ApiController]
    public class PipelineController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public PipelineController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("{projectId}/pipelines")]
        public async Task<string> GetProjectPipelines(int projectId)
        {
            return await _projectService.GetAllProjectPipelines(projectId);
        }
    }
}