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

    public async Task<string> CreateProject(string name, string description, string path,  bool initializeWithReadme)
    {
        return await _projectRepository.CreateProject(name, description, path, initializeWithReadme);
    }
}