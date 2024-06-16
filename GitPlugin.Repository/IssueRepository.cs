namespace GitPlugin.Repository;

using GitPlugin.Core.Repository;
using System;
using System.Text;
using System.Text.Json;
using System;
using System.Net.Http;
using DotNetEnv;
using System.IO;

public class IssueRepository : IIssueRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _privateToken;
    private const string BaseUrl = "https://gitlab.com/api/v4/projects";
    
    public IssueRepository()
    {
        DotNetEnv.Env.Load();
        DotNetEnv.Env.TraversePath().Load();
        _privateToken = DotNetEnv.Env.GetString("GITLAB_PAT");
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("PRIVATE-TOKEN",_privateToken);
    }
    
    public async Task<string> GetProjectIssues(int projectId)
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