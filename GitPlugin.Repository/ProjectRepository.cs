
namespace GitPlugin.Repository;

using GitPlugin.Core.Repository;
using System;
using System.Text;
using System.Text.Json;
using System;
using System.Net.Http;

public class ProjectRepository : IProjectRepository
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://gitlab.com/api/v4/projects";
    private const string PrivateToken = "glpat-2pSgqLqM-2vkyEhxF_xb";
    
    public ProjectRepository()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("PRIVATE-TOKEN",PrivateToken);
    }
    
    public async Task<string> SelectAll()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("?owned=true");
        response.EnsureSuccessStatusCode();
        var result =await response.Content.ReadAsStringAsync(); 
        return result;
    }
    public async Task<string> SelectAllProjectIssues(int projectId)
    {
        string requestUrl = $"projects/{projectId}/issues";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync();
        
        var options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        var issues = JsonSerializer.Deserialize<object>(responseContent);
        return JsonSerializer.Serialize(issues, options);
    }

    
    public async Task<string> CreateProject(string name, string description, string path, bool initializeWithReadme)
    {
        var postData = new
        {
            name,
            description,
            path,
            initialize_with_readme = initializeWithReadme
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("", content); 
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> SelectAllMergeRequest(int projectId)
    {
        string requestUrl = $"projects/{projectId}/merge_requests?state=all";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> SelectAllPipelines(int projectId)
    {
        string requestUrl = $"projects/{projectId}/pipelines";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> SelectAllReleases(int projectId)
    {
        string requestUrl = $"projects/{projectId}/releases";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> SelectAllLanguages(int projectId)
    {
        string requestUrl = $"projects/{projectId}/languages";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> SelectProjectContributors(int projectId)
    {
        string requestUrl = $"projects/{projectId}/repository/contributors";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> CreateIssue(int projectId, string title, string description)
    {
        string requestUrl = $"projects/{projectId}/issues";

        var postData = new
        {
            title,
            description
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<string> DeleteIssue(int projectId, int issueId)
    {
        string requestUrl = $"projects/{projectId}/issues/{issueId}";

        var response = await _httpClient.DeleteAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UpdateIssueState(int projectId, int issueId, string stateEvent)
    {
        string requestUrl = $"projects/{projectId}/issues/{issueId}";

        var postData = new
        {
            state_event = stateEvent
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> DeleteProject(int projectId)
    {
        string requestUrl = $"{projectId}";

        HttpResponseMessage response = await _httpClient.DeleteAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }


    public async Task<string> UpdateProject(int projectId, string name, string description, string path)
    {
        string requestUrl = $"{projectId}";

        var postData = new
        {
            name,
            description,
            path
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync(requestUrl, content);
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

}

