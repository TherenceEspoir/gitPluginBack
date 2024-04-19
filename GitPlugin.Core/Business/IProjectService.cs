namespace GitPlugin.Core.Business;

public interface IProjectService
{
    Task<string> GetAllProjects();

    Task<string> CreateProject(string name, string description, string path,
        bool initializeWithReadme);

}