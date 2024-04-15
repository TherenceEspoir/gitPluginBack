namespace GitPlugin.Repository;

using System.Net.Http.Json;
using GitPlugin.Core.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly HttpClient _httpClient;

    public ProjectRepository()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://gitlab.com/api/v4/projects?owned=true");
        _httpClient.DefaultRequestHeaders.Add("PRIVATE-TOKEN", "glpat-2pSgqLqM-2vkyEhxF_xb");
    }
    
    public async Task<string> SelectAll()
    {
        
        HttpResponseMessage response = await _httpClient.GetAsync("");
        
        
        response.EnsureSuccessStatusCode();

        var coucou =await response.Content.ReadAsStringAsync(); 
        
        return coucou;
    }
}