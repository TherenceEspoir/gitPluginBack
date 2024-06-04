namespace GitPlugin.Core.Business;

public interface IProjectService
{
    Task<string> GetAllProjects();

    Task<string> GetAllProjectIssues(int projectId);

    Task<string> CreateProject(string name, string description, string path,
        bool initializeWithReadme);

    Task<string> GetAllProjectMergeRequest(int projectId);

    Task<string> GetAllProjectPipelines(int projectId);
    
    Task<string> GetAllProjectReleases(int projectId);
    
    Task<string> GetAllLanguages(int projectId);
    
    Task<string> GetAllContributors(int projectId);
    Task<string> CreateIssue(int projectId, string title, string description);
    Task<string> DeleteIssue(int projectId, int issueId);
    Task<string> UpdateIssueState(int projectId, int issueId, string stateEvent);
}