using TracksifyAPI.Models;

namespace TracksifyAPI.Interfaces
{
    // Interface for the declarations of CRUD operations 
    public interface IProjectUpdateRepository
    {
        Task<List<ProjectUpdate>> GetAllProjectUpdateAsync();
        Task<ProjectUpdate?> GetProjectUpdateByIdAsync(Guid projectUpdateId);
        Task<ProjectUpdate> CreateProjectUpdateAsync(ProjectUpdate projectUpdateToCreate);
        Task<bool> ProjectUpdateExistsAsync(Guid projectUpdateId);
    }
}
