namespace GitPlugin.Repository;

using GitPlugin.Core.Repository;
using System;
using System.Text;
using System.Text.Json;
using System;
using System.Net.Http;
using DotNetEnv;
using System.IO;

public class MergeRequestRepository : IMergeRequestRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _privateToken;
    private const string BaseUrl = "https://gitlab.com/api/v4/projects";
    
    public MergeRequestRepository()
    {
        DotNetEnv.Env.Load();
        DotNetEnv.Env.TraversePath().Load();
        _privateToken = DotNetEnv.Env.GetString("GITLAB_PAT");
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("PRIVATE-TOKEN",_privateToken);
    }
    
    public async Task<string> GetAllProjectMergeRequest(int projectId)
    {
        string requestUrl = $"projects/{projectId}/merge_requests?state=all";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<string> CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title, string description)
    {
        string requestUrl = $"projects/{projectId}/merge_requests";

        var postData = new
        {
            source_branch = sourceBranch,
            target_branch = targetBranch,
            title,
            description
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<string> DeleteMergeRequest(int projectId, int mergeRequestId)
    {
        string requestUrl = $"projects/{projectId}/merge_requests/{mergeRequestId}";

        var response = await _httpClient.DeleteAsync(requestUrl);
        if (!response.IsSuccessStatusCode)
        {
            string errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error deleting merge request: {response.StatusCode}, {errorContent}");
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UpdateMergeRequest(int projectId, int mergeRequestId, string title, string description)
    {
        string requestUrl = $"projects/{projectId}/merge_requests/{mergeRequestId}";

        var postData = new
        {
            title,
            description
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetMergeRequestById(int projectId, int mergeRequestId)
    {
        string requestUrl = $"projects/{projectId}/merge_requests/{mergeRequestId}";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}