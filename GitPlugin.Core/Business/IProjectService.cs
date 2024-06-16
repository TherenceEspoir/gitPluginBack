namespace GitPlugin.Core.Business;

public interface IProjectService
{
    Task<string> GetAllProjects();
    Task<string> CreateProject(string name, string description, string path,
        bool initializeWithReadme);
    Task<string> GetAllProjectPipelines(int projectId);
    Task<string> GetAllProjectReleases(int projectId);
    Task<string> GetAllLanguages(int projectId);
    Task<string> GetAllContributors(int projectId);
    Task<string> DeleteProject(int projectId);
    Task<string> UpdateProject(int projectId, string name, string description);
    Task<string> GetProjectById(int projectId);
}