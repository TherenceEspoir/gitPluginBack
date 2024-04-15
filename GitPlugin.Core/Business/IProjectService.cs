namespace GitPlugin.Core.Business;

public interface IProjectService
{
    Task<string> GetAllProjects();
}