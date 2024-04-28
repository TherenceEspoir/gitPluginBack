namespace GitPlugin.Core.Repository;

public interface IProjectRepository
{
    Task<string> SelectAll();
    
    Task<string> SelectAllProjectIssues(int projectId);

    Task<string> CreateProject(string name, string description, string path,  bool initializeWithReadme);
    
    Task<string> SelectAllMergeRequest(int projectId);

}