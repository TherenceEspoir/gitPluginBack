namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<string> GetAll()
        {
            return await _projectService.GetAllProjects();
        }
        
        [AllowAnonymous]
        [HttpGet("{projectId}")]
        public async Task<string> GetProjectById(int projectId)
        {
            return await _projectService.GetProjectById(projectId);
        }
        
        [HttpPost]
        [Route("")]
        public async Task<string> CreateProject(string name, string description, string path, bool initializeWithReadme)
        {
            var token = Request.Headers["Authorization"].ToString();
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