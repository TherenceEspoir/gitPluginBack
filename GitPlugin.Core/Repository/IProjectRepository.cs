namespace GitPlugin.Core.Repository;

public interface IProjectRepository
{
    Task<string> SelectAll();
}