using Microsoft.EntityFrameworkCore;
using TracksifyAPI.Data;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Models;

namespace TracksifyAPI.Repositories
{

    /**
     * ProjectUpdateRepository - Implements the IProjectUpdateRepository
     */
    public class ProjectUpdateRepository : IProjectUpdateRepository
    {
        private readonly TracksifyDataContext _context;
        public ProjectUpdateRepository(TracksifyDataContext context)
        {
            _context = context;
        }

        /**
         * CreateProjectUpdateAsync - Creates a ProjectUpdate asynchronously
         * @projectUpdateToCreate - ProjectUpdate to be created
         * Return: Returns a ProjectUpdate object
         */
    public async Task<ProjectUpdate> CreateProjectUpdateAsync(ProjectUpdate projectUpdateToCreate)
        {
            await _context.ProjectUpdates.AddAsync(projectUpdateToCreate);
            await _context.SaveChangesAsync();
            return projectUpdateToCreate;
        }

        /**
         * GetAllProjectUpdateAsync - Get's all ProjectUpdates asynchronously
         * Return: Returns a list of all ProjectUpdates
         */
        public async Task<List<ProjectUpdate>> GetAllProjectUpdateAsync()
        {
            return await _context.ProjectUpdates.ToListAsync();
        }

        /**
         * GetProjectUpdateByIdAsync - Get a single projectUpdate by Id
         * Return: return the projectUpdate with the associated ID
         */
        public async Task<ProjectUpdate?> GetProjectUpdateByIdAsync(Guid projectUpdateId)
        {
            return await _context.ProjectUpdates.FindAsync(projectUpdateId);
        }

        /**
         * GetProjectUpdateByProjectIdAsync - GetProjectUpdate by projectId
         * Return: returns a list of projectUpdates for the specific user
         */
        public async Task<List<ProjectUpdate>> GetProjectUpdateByProjectIdAsync(Guid projectId)
        {
            return await _context.ProjectUpdates.Where(p => p.ProjectId == projectId).ToListAsync();
        }

        /**
         * GetProjectUpdateByUserIdAsync - GetProjectUpdate by userId
         * Return: returns a list of projectUpdates for the specific user
         */
        public async Task<List<ProjectUpdate>> GetProjectUpdateByUserIdAsync(Guid userId)
        {
            return await _context.ProjectUpdates.Where(p => p.UserId == userId).ToListAsync();
        }

        /**
         * GetUserProjectUpdateForSingleProjectByProjectIdAsync - GetProjectUpdate by userId for a particular project
         * Return: returns a list of projectUpdates for the specific user
         */
        public async Task<List<ProjectUpdate>> GetUserProjectUpdateForSingleProjectByProjectIdAsync(Guid userId, Guid projectId)
        {
            return await _context.ProjectUpdates
                                                .Where(p => p.UserId == userId && p.ProjectId == projectId)
                                                .ToListAsync();
        }
        /**
         * ProjectUpdateExistsAsync - Checks if ProjectUpdate Exists
         * Return: returns a bool
         */
        public async Task<bool> ProjectUpdateExistsAsync(Guid projectUpdateId)
        {
            return await _context.ProjectUpdates.AnyAsync(p => p.ProjectUpdateId == projectUpdateId);
        }

        /**
         * ProjectUpdateExistsForThatDay - Checks that a projectUpdate has been created for that day
         * Return: returns true if it has been created
         */
        public async Task<bool> ProjectUpdateExistsForThatDay(Guid userId, Guid projectId, DateTime dateCreated)
        {
            return await _context.ProjectUpdates
                                                .AnyAsync(p => p.UserId == userId &&
                                                               p.ProjectId == projectId && 
                                                               p.DateCreated.Date == dateCreated.Date);
        }
    }
}
