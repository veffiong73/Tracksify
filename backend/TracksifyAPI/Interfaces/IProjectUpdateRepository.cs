using TracksifyAPI.Models;

namespace TracksifyAPI.Interfaces
{
    // Interface for the declarations of CRUD operations 
    public interface IProjectUpdateRepository
    {
        Task<List<ProjectUpdate>> GetAllProjectUpdateAsync();
        Task<ProjectUpdate?> GetProjectUpdateByIdAsync(Guid projectUpdateId);
        Task<List<ProjectUpdate>> GetProjectUpdateByProjectIdAsync(Guid projectId);
        Task<List<ProjectUpdate>> GetProjectUpdateByUserIdAsync(Guid userId);
        Task<List<ProjectUpdate>> GetUserProjectUpdateForSingleProjectByProjectIdAsync(Guid userId, Guid projectId);
        Task<ProjectUpdate> CreateProjectUpdateAsync(ProjectUpdate projectUpdateToCreate);
        Task<bool> ProjectUpdateExistsForThatDay(Guid userId, Guid projectId, DateTime dateCreated);
        Task<bool> ProjectUpdateExistsAsync(Guid projectUpdateId);

    }
}
