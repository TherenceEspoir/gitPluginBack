namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        [HttpGet]
        public async Task<string> GetAll()
        {
            return await _projectService.GetAllProjects();
        }
        /*
        [HttpGet("{projectId}/issues")]
        public async Task<string> GetProjectIssues(int projectId)
        {
            return await _projectService.GetAllIssues(projectId);
        }
        */
        
        [HttpGet]
        [Route("{projectId}/issues")] 
        public async Task<string> GetProjectIssues(int projectId)
        {
            return await _projectService.GetAllProjectIssues(projectId);
        }
        
        [HttpPost]
        [Route("")]
        public async Task<string> CreateProject(string name, string description, string path, bool initializeWithReadme)
        {
            return await _projectService.CreateProject(name, description, path, initializeWithReadme);
        }
    }
}