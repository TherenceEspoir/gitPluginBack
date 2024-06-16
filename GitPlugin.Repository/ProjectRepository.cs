
namespace GitPlugin.Repository;

using GitPlugin.Core.Repository;
using System;
using System.Text;
using System.Text.Json;
using System;
using System.Net.Http;
using DotNetEnv;
using System.IO;

public class ProjectRepository : IProjectRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _privateToken;
    private const string BaseUrl = "https://gitlab.com/api/v4/projects";
    
    public ProjectRepository()
    {
        DotNetEnv.Env.Load();
        DotNetEnv.Env.TraversePath().Load();
        _privateToken = DotNetEnv.Env.GetString("GITLAB_PAT");
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("PRIVATE-TOKEN",_privateToken);
    }

    public async Task<string> SelectAll()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("?owned=true");
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        
        var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        var filteredProjects = new List<object>();

        foreach (var project in root.EnumerateArray())
        {
            var filteredProject = new {
                id = project.GetProperty("id").GetInt32(),
                name = project.GetProperty("name").GetString(),
                description = project.GetProperty("description").GetString(),
                path = project.GetProperty("path").GetString(),
                created_at = project.GetProperty("created_at").GetString()
            };
            filteredProjects.Add(filteredProject);
        }

        return JsonSerializer.Serialize(filteredProjects);
    }
    
    
    public async Task<string> SelectAllProjectIssues(int projectId)
    {
        string requestUrl = $"projects/{projectId}/issues";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        
        var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        var filteredIssues = new List<object>();

        foreach (var issue in root.EnumerateArray())
        {
            var filteredIssue = new {
                iid = issue.GetProperty("iid").GetInt32(),
                title = issue.GetProperty("title").GetString(),
                state = issue.GetProperty("state").GetString(),
                created_at = issue.GetProperty("created_at").GetString(),
                author = new {
                    username = issue.GetProperty("author").GetProperty("username").GetString(),
                    avatar_url = issue.GetProperty("author").GetProperty("avatar_url").GetString()
                }
            };
            filteredIssues.Add(filteredIssue);
        }

        return JsonSerializer.Serialize(filteredIssues);
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

        HttpResponseMessage response = await _httpClient.DeleteAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UpdateIssueState(int projectId, int issueId, string title, string description)
    {
        string requestUrl = $"projects/{projectId}/issues/{issueId}";

        var postData = new
        {
            title=title,
            description=description
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> DeleteProject(int projectId)
    {
        string requestUrl = $"projects/{projectId}";

        HttpResponseMessage response = await _httpClient.DeleteAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }


    public async Task<string> UpdateProject(int projectId, string name, string description)
    {

        string requestUrl = $"projects/{projectId}";

        var postData = new
        {
            name,
            description
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

    public async Task<string> GetProjectById(int projectId)
    {
        string requestUrl = $"projects/{projectId}";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<string> GetIssueById(int projectId, int issueId)
    {
        string requestUrl = $"projects/{projectId}/issues/{issueId}";
        var response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();

        var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        var filteredIssue = new {
            title = root.GetProperty("title").GetString(),
            description = root.GetProperty("description").GetString()
        };

        return JsonSerializer.Serialize(filteredIssue);
    }
    
}

