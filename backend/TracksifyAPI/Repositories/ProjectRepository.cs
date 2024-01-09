using Microsoft.EntityFrameworkCore;
using TracksifyAPI.Data;
using TracksifyAPI.Helpers;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Models;

namespace TracksifyAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TracksifyDataContext _context;
        public ProjectRepository(TracksifyDataContext context)
        {
            _context = context;            
        }
        public async Task<List<Project>> GetAllProjectsAsync(ProjectQueryObject query)
        {
            // gets all the projects from the database context
            // and stores it in the variable
            // AsQueryable method converts ienumerable collection to iqueryable
            // Queryable means being you can ask it for data and it can return data
//            

            // if we pass in the name or status, it should be able to return all
            // all projects with that name or status respectively

          /*  if (!string.IsNullOrWhiteSpace(query.ProjectName))
            {
                projects = projects.Where(p => p.ProjectName.Contains(query.ProjectName));
            }*/

            
           
 /*           if (query.ProjectStatus != null)
            {
                    projects = projects.Where(p => p.ProjectStatus == (query.ProjectStatus));
            }

            if (query.StartDate != null)
            {
                projects = projects.Where(p => p.StartDate == (query.StartDate));
            }

            if (query.DueDate != null)
            {
                projects = projects.Where(p => p.DueDate == (query.DueDate));
            }*/
            var projects = _context.Projects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.ProjectName))
            {
                projects = projects.Where(p => p.ProjectName.Contains(query.ProjectName));
            }

            if (!DateTime.ToString.)
            return await projects.ToListAsync();

        }
        public async Task<Project> GetProjectByProjectIdASync(Guid projectId)
        {
            return await _context.Projects.FindAsync(projectId);
        }

        public async Task<ICollection<User>> GetProjectAssigneesASync(Guid projectId)
        {
            // get all projects from db context, get project matching project id and bring out the assignees in that instance
            var projectassignees = await _context.Projects.Where(p =>p.ProjectId == projectId).Select(o => o.ProjectAssignees).FirstOrDefaultAsync();

            if (projectassignees == null)
            {
                throw new Exception("Project not found");
            }

            return projectassignees;
        }

/*        public async Task<Project> GetProjectByStartDateASync(string startDate)
        {
            return await _context.Projects.FindAsync(startDate);
        }
*/
        public async Task<Project> GetProjectByUserIdASync(Guid userId)
        {
            return await _context.Projects.FindAsync(userId);
        }

        public async Task<bool> ProjectExistsASync(Guid projectId)
        {
            return await _context.Projects.AnyAsync(p => p.ProjectId == projectId);

        }

        /*        public async Task<bool> ProjectExistsASync(ProjectQueryObject query)
                {
                    var projects = _context.Projects.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(query.ProjectName))
                    {
                        projects = projects.Where(p => p.ProjectName.Contains(query.ProjectName));
                    }

                    if (query.ProjectId != null)
                    {
                        projects = projects.Where(p => p.ProjectId == query.ProjectId);
                    }
                    return await projects.AnyAsync();
                }*/

        public async Task<Project> ProjectASync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
