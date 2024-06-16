namespace GitPlugin.Business;

using GitPlugin.Core.Business;
using GitPlugin.Core.Repository;
using GitPlugin.Repository;

public class MergeService : IMergeService
{
    private readonly IMergeRequestRepository _mergerequestRepository;
    
    public MergeService(IMergeRequestRepository mergerequestRepository)
    {
        _mergerequestRepository = mergerequestRepository;
    }
    
    public async Task<string> GetAllProjectMergeRequest(int projectId)
    {
        return await _mergerequestRepository.GetAllProjectMergeRequest(projectId);
    }

    public async Task<string> CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title, string description)
    {
        return await _mergerequestRepository.CreateMergeRequest(projectId, sourceBranch, targetBranch, title,
            description);
    }

    public async Task<string> DeleteMergeRequest(int projectId, int mergeRequestId)
    {
        return await _mergerequestRepository.DeleteMergeRequest(projectId, mergeRequestId);
    }

    public async Task<string> UpdateMergeRequest(int projectId, int mergeRequestId, string title, string description)
    {
        return await _mergerequestRepository.UpdateMergeRequest(projectId, mergeRequestId, title, description);
    }

    public async Task<string> GetMergeRequestById(int projectId, int mergeRequestId)
    {
        return await _mergerequestRepository.GetMergeRequestById(projectId,mergeRequestId);
    }
}