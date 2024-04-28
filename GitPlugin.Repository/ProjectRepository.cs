namespace GitPlugin.Repository;

using GitPlugin.Core.Repository;
using System;
using System.Text;
using System.Text.Json;

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
        return await response.Content.ReadAsStringAsync();
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

}