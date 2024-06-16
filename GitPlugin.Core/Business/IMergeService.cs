namespace GitPlugin.Core.Business;

public interface IMergeService
{
    Task<string> GetAllProjectMergeRequest(int projectId);
    Task<string> CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title, string description);
    Task<string> DeleteMergeRequest(int projectId, int mergeRequestId);
    Task<string> UpdateMergeRequest(int projectId, int mergeRequestId, string title, string description);
    Task<string> GetMergeRequestById(int projectId, int mergeRequestId);
}