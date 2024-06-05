namespace GitPlugin.Core.Repository;

public interface IProjectRepository
{
    Task<string> SelectAll();
    
    Task<string> SelectAllProjectIssues(int projectId);

    Task<string> CreateProject(string name, string description, string path,  bool initializeWithReadme);
    
    Task<string> SelectAllMergeRequest(int projectId);
    
    Task<string> SelectAllPipelines(int projectId);
    
    Task<string> SelectAllReleases(int projectId);
    
    Task<string> SelectAllLanguages(int projectId);
    
    Task<string> SelectProjectContributors(int projectId);
    
    Task<string> CreateIssue(int projectId, string title, string description);
    Task<string> DeleteIssue(int projectId, int issueId);
    Task<string>UpdateIssueState(int projectId, int issueId, string stateEvent);
    Task<string> DeleteProject(int projectId);
    Task<string> UpdateProject(int projectId, string name, string description, string path);
    Task<string> CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title, string description);
    Task<string> DeleteMergeRequest(int projectId, int mergeRequestId);
    Task<string> UpdateMergeRequest(int projectId, int mergeRequestId, string title, string description);
}