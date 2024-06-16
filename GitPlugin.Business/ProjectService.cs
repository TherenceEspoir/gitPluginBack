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

    public async Task<string> GetAllProjectPipelines(int projectId)
    {
        return await _projectRepository.SelectAllPipelines(projectId);
    }

    public async Task<string> GetAllProjectReleases(int projectId)
    {
        return await _projectRepository.SelectAllReleases(projectId);
    }

    public async Task<string> GetAllLanguages(int projectId)
    {
        return await _projectRepository.SelectAllLanguages(projectId);
    }

    public async Task<string> GetAllContributors(int projectId)
    {
        return await _projectRepository.SelectProjectContributors(projectId);
    }
    
    public async Task<string> DeleteProject(int projectId)
    {
        return await  _projectRepository.DeleteProject( projectId);
    }

    public async Task<string> UpdateProject(int projectId, string name, string description)
    {
        return await _projectRepository.UpdateProject(projectId, name, description);
    }
    
    public async Task<string> GetProjectById(int projectId)
    {
        return await _projectRepository.GetProjectById(projectId);
    }
}