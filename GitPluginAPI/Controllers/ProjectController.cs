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
        
        [HttpGet]
        [Route("{projectId}/issues")] 
        public async Task<string> GetProjectIssues(int projectId)
        {
            return await _projectService.GetAllProjectIssues(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/merge_request")] 
        public async Task<string> GetProjectMergeRequest(int projectId)
        {
            return await _projectService.GetAllProjectMergeRequest(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/pipelines")] 
        public async Task<string> GetProjectPipelines(int projectId)
        {
            return await _projectService.GetAllProjectPipelines(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/releases")] 
        public async Task<string> GetProjectReleases(int projectId)
        {
            return await _projectService.GetAllProjectReleases(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/languages")] 
        public async Task<string> GetProjectLanguages(int projectId)
        {
            return await _projectService.GetAllLanguages(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/contributors")] 
        public async Task<string> GetProjectContributors(int projectId)
        {
            return await _projectService.GetAllContributors(projectId);
        }
        
        [HttpPost]
        [Route("")]
        public async Task<string> CreateProject(string name, string description, string path, bool initializeWithReadme)
        {
            return await _projectService.CreateProject(name, description, path, initializeWithReadme);
        }
    }
}