namespace GitPlugin.Core.Repository;

public interface IIssueRepository
{
    Task<string> CreateIssue(int projectId, string title, string description);
    Task<string> DeleteIssue(int projectId, int issueId);
    Task<string> UpdateIssueState(int projectId, int issueId, string title, string description);
    Task<string> GetIssueById(int projectId, int issueId);
    Task<string> GetProjectIssues(int projectId);
}