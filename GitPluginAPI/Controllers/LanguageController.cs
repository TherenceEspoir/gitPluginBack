namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;

    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public LanguageController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("{projectId}/languages")]
        public async Task<string> GetProjectLanguages(int projectId)
        {
            return await _projectService.GetAllLanguages(projectId);
        }
    }
}