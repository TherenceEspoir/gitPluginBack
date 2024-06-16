namespace GitPlugin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GitPlugin.Business;
    using GitPlugin.Core.Business;
    using Microsoft.AspNetCore.Authorization;

    
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MergeRequestController : ControllerBase
    {
       
        private readonly IMergeService _mergeService;

        public MergeRequestController(IMergeService mergeService)
        {
            _mergeService = mergeService;
        }

        [HttpGet]
        [Route("{projectId}/merge_request")]
        public async Task<string> GetProjectMergeRequest(int projectId)
        {
            return await _mergeService.GetAllProjectMergeRequest(projectId);
        }
        
        [HttpGet]
        [Route("{projectId}/merge_request/{mergeRequestId}")]
        public async Task<string> GetMergeRequestById(int projectId, int mergeRequestId)
        {
            return await _mergeService.GetMergeRequestById(projectId, mergeRequestId);
        }

        [HttpPost]
        [Route("{projectId}/merge_request")]
        public async Task<string> CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title, string description)
        {
            return await _mergeService.CreateMergeRequest(projectId, sourceBranch, targetBranch, title, description);
        }

        [HttpDelete]
        [Route("{projectId}/merge_request/{mergeRequestId}")]
        public async Task<string> DeleteMergeRequest(int projectId, int mergeRequestId)
        {
            return await _mergeService.DeleteMergeRequest(projectId, mergeRequestId);
        }

        [HttpPut]
        [Route("{projectId}/merge_request/{mergeRequestId}")]
        public async Task<string> UpdateMergeRequest(int projectId, int mergeRequestId, string title, string description)
        {
            return await _mergeService.UpdateMergeRequest(projectId, mergeRequestId, title, description);
        }
    }
}