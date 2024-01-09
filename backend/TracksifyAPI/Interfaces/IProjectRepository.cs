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
        Task<Project> GetProjectByProjectIdASync(Guid projectId);
        Task<Project> GetProjectByUserIdASync(Guid userId);
       // Task<Project> GetProjectByStartDateASync(string startDate);
        Task<bool> ProjectExistsASync(Guid projectId);

        //Task<bool> ProjectExistsASync(ProjectQueryObject query);
        Task<ICollection<User>> GetProjectAssigneesASync(Guid projectId);

        // POST Methods
        Task<Project> ProjectASync(Project project);
    }
}
