namespace GitPlugin.Business;

using GitPlugin.Core.Business;
using GitPlugin.Core.Repository;

public class IssueService : IIssueService
{
    private readonly IIssueRepository _issueRepository;
    
    public IssueService(IIssueRepository issueRepository)
    {
        _issueRepository = issueRepository;
    }
    public async Task<string> CreateIssue(int projectId, string title, string description)
    {
        return  await _issueRepository.CreateIssue(projectId, title, description);
    }
    public async Task<string> DeleteIssue(int projectId, int issueId)
    {
        return await _issueRepository.DeleteIssue(projectId, issueId);
    }
    public async Task<string> UpdateIssueState(int projectId, int issueId, string title, string description)
    {
        return await _issueRepository.UpdateIssueState(projectId, issueId,title,description);
    }
    public async Task<string> GetIssueById(int projectId, int issueId)
    {
        return await _issueRepository.GetIssueById(projectId, issueId);
    }
    public async Task<string> GetProjectIssues(int projectId)
    {
        return await _issueRepository.GetProjectIssues(projectId);
    }
}