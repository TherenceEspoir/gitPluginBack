namespace GitPlugin.Core.Repository;

public interface IProjectRepository
{
    Task<string> SelectAll();
    Task<string> CreateProject(string name, string description, string path,  bool initializeWithReadme);
    Task<string> SelectAllPipelines(int projectId);
    Task<string> SelectAllReleases(int projectId);
    Task<string> SelectAllLanguages(int projectId);
    Task<string> SelectProjectContributors(int projectId);
    Task<string> DeleteProject(int projectId);
    Task<string> UpdateProject(int projectId, string name, string description);
    Task<string> GetProjectById(int projectId);
}