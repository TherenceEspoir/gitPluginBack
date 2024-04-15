namespace GitPlugin.Business;

using GitPlugin.Core.Business;
using GitPlugin.Core.Repository;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<string> GetAllProjects()
    {
        return await _projectRepository.SelectAll();
    }
    
}