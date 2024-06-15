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
        
        [HttpPost]
        [Route("")]
        public async Task<string> CreateProject(string name, string description, string path, bool initializeWithReadme)
        {
            return await _projectService.CreateProject(name, description, path, initializeWithReadme);
        }
        
        [HttpDelete]
        [Route("{projectId}")]
        public async Task<string> DeleteProject(int projectId)
        {
            return await _projectService.DeleteProject(projectId);
        }

        // Mettre à jour un projet
        [HttpPut]
        [Route("{projectId}")]
        public async Task<string> UpdateProject(int projectId, string name, string description)
        {
            return await _projectService.UpdateProject(projectId, name, description);
        }
        
    }
}