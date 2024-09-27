using TracksifyAPI.Dtos.Project;
using TracksifyAPI.Dtos.User;
using TracksifyAPI.Helpers;
using TracksifyAPI.Models;

namespace TracksifyAPI.Interfaces
{
    // This repository interface defines the CRUD methods for interacting
    // with "project" data in the database
    public interface IProjectRepository
    {
        // GET Methods
        Task<List<Project>> GetAllProjectsAsync(ProjectQueryObject query);
        Task<Project?> GetProjectByProjectIdASync(Guid projectId);
        Task<List<Project>> GetProjectByUserIdASync(Guid userId);
        Task<bool> ProjectExistsASync(Guid projectId);
        Task<ICollection<User>> GetProjectAssigneesASync(Guid projectId);
        Task<Project?> UpdateProjectASync(Guid projectId, Project project);

        // POST Methods
        Task<Project> CreateProjectASync(Project project);
        Task DeleteProjectAsync(Project project);

    }
}
